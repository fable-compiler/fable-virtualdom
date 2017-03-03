// If you are using the sample in standalone please switch the import lines
// #r "node_modules/fable-core/Fable.Core.dll"
// #r "node_modules/fable-arch/Fable.Arch.dll"
// Imports for docs site mode
#r "../../node_modules/fable-core/Fable.Core.dll"
#r "../../node_modules/fable-arch/Fable.Arch.dll"

open System
open Fable.Arch
open Fable.Arch.App
open Fable.Arch.App.AppApi
open Fable.Arch.Html

open Fable.Import
open Fable.Import.Browser

type Messages = Tick of DateTime
type Model = CurrentTime of DateTime

let initial() : Model = CurrentTime DateTime.Now 

let update (CurrentTime time) (Tick next) = CurrentTime next 

type Time = 
    | Hour of int
    | Minute of int
    | Second of int

let view (CurrentTime time) = 
    // Svg helpers
    let line x = svgElem "line" x
    let x1 x = attribute "x1" x
    let x2 x = attribute "x2" x
    let y1 x = attribute "y1" x
    let y2 x = attribute "y2" x
    let strokeWidth x = attribute "stroke-width" x

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
    
    let handTop n color length fullRound = 
        let revolution = float n
        let angle = 2.0 * Math.PI * (revolution / fullRound)
        let handX = (50.0 + length * cos (angle - Math.PI / 2.0))
        let handY = (50.0 + length * sin (angle - Math.PI / 2.0))
        circle [ cx (string handX); cy (string handY); r "2"; fill color ] []


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


let tickProducer push =
    window.setInterval((fun _ ->
        push (Tick DateTime.Now) 
    ), 1000) |> ignore
    push (Tick DateTime.Now)


createSimpleApp (initial()) view update Virtualdom.createRender
|> withStartNodeSelector "#sample"
|> withProducer tickProducer
|> start