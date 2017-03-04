# Clock sample in SVG 

In this sample, we will create a simple clock app and render it using svg. The only thing we want to keep track of in our app is the current time. This becomes our `Model`
```fsharp
type Model = CurrentTime of DateTime
```
Whenever the clock ticks, we want to dispatch a message (we also call it an "Action") containing the new time and rerender the clock. So our `Messages` type becomes:
```fsharp
type Messages = Tick of DateTime
```
When a message is dispatched, we want to update our model to the new time, so we simply set the the `CurrentTime` of the `Model` to whatever we get from the `Tick`. The update function becomes:
```fsharp
// Model -> Messages -> Model
let update (CurrentTime time) (Tick next) = CurrentTime next 
```
Now to the slightly harder part. The actual rendering and the `view` function. Our view will take the `Model` and return a clock in svg. A clock has a couple of parts:
 - The background circle
 - The clocks hands, one for seconds, one for minutes and one for hours, we will draw there in descending lengths, respectively.
 - The "top" of the hand is a little circle as well
 - The center of the clock is another circle

 Because the background circle will be the container (parent) for the other elements, we will leave for now and write small (child) functions of the parts of the circle.

 I will first define a helper type `Time` so we can pass it as argument for `clockHand`. `Time` tells us if we are dealing with seconds, minutes or hours. We define it like this:
 ```fsharp
 type Time = 
    | Hour of int
    | Minute of int
    | Second of int
 ```

We start with the hands of the clock:
```fsharp
// Time -> string -> float -> float
let clockHand time color width length = 
    let clockPercentage = 
        match time with 
        | Hour n -> (float n) / 12.0
        | Second n -> (float n) / 60.0
        | Minute n -> (float n) / 60.0
    let angle = 2.0 * Math.PI * clockPercentage
    let handX = (50.0 + length * cos (angle - Math.PI / 2.0))
    let handY = (50.0 + length * sin (angle - Math.PI / 2.0))
    line [ x1 "50"; y1 "50"; x2 (string handX); y2 (string handY); stroke color; strokeWidth width ] []
```
First the `clockPercentage`. This tells us "how much of this circle / clock the hand has already passed". For example if we are dealing with minutes and it is 30 minutes (out of 60) then the `clockPercentage` is 0.5 (50%). I use this percentage again to calculate the `angle` or how many radians I passed (`angle` = `clockPercentage` * 2.0 * Math.PI)

In the next two lines, I substract Pi/2 from the angle. Because if I have 0 radians, Then I will already be on minute 15 (out of the 60) on the clock. So I "go back" 15 minutes (= Pi/2). Lastly, we return a line element that starts from the center of the cicle (x1=50, y1=50) to the point we just calculated.

Next is the "top" of the hands which will be small circle. It follows the same logic as will the line.
```fsharp
let handTop n color length fullRound = 
    let revolution = float n
    let angle = 2.0 * Math.PI * (revolution / fullRound)
    let handX = (50.0 + length * cos (angle - Math.PI / 2.0))
    let handY = (50.0 + length * sin (angle - Math.PI / 2.0))
    circle [ cx (string handX); cy (string handY); r "2"; fill color ] []
```
Lastly, we return the parent SVG element with start point of (x=0, y=0) and size of (height=100) and (width=100). Thats why we said the center of the circle is 50. When the SVG element is rendered, it takes 300px of the page. The next child element is the background circle and inside it we generate the elements of the clock. The last child is the little circle in the center.
```fsharp
svg [ viewBox "0 0 100 100"; width "300px" ]
  [ circle [ cx "50"; cy "50"; r "45"; fill "#0B79CE" ] []
    // Seconds
    clockHand (Second time.Second) "#023963" "1" 40.0 
    handTop time.Second "#023963" 40.0 60.0
    // Minutes
    clockHand (Minute time.Minute) "white" "2" 35.0
    handTop time.Minute "white" 35.0 60.0
    // Hours
    clockHand (Hour time.Hour) "lightgreen" "2" 25.0
    handTop time.Hour "lightgreen" 25.0 12.0
    // circle in the center
    circle [ cx "50"; cy "50"; r "3"; fill "#0B79CE"; stroke "#023963"; strokeWidth "1"  ] []
  ]
```

That's not all. We still need a way to dispatch the message to app informing that the time has changed. For that, we use a `producer`. Producers help us send messages that do not originate from user interaction but from side-effects. We define it like this:
```fsharp
let tickProducer push =
    window.setInterval((fun _ ->
        push (Tick DateTime.Now) 
    ), 1000) |> ignore
    push (Tick DateTime.Now)
```

This is a repeating function that runs every second and automtically dispatches a `Tick` message to the app. Effectively updating the model and renderer the clock.

Run the app and register the producer:
```
createSimpleApp (initial()) view update Virtualdom.createRender
|> withStartNodeSelector "#sample"
|> withProducer tickProducer
|> start
```