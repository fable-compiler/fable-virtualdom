#r "node_modules/fable-core/Fable.Core.dll"
#r "node_modules/fable-arch/Fable.Arch.dll"


open Fable.Core
open Fable.Core.JsInterop

open Fable.Arch
open Fable.Arch.App
open Fable.Arch.Html

open System

type Card = {
    Id : int
    ImgUrl : string
    Selected : bool
    MatchFound : bool
}

type Model = {
    Cards : Card list
    FirstSelection : int option
    SecondSelection : int option
}

type Actions =
    | SelectCard of int 
    | StartNewGame
    | NoOp

let random = new Random();

let getCards() = 
    let images = [ "violin"; "electric-guitar"; "headphones"; "piano"; "saxophone";  "trumpet";"turntable";"bass-guitar" ]
    images 
    |> List.append images
    |> List.sortBy (fun img -> random.Next())
    |> List.map (sprintf "images/%s.png")
    |> List.mapi (fun index img -> { Id = index; ImgUrl = img; Selected = false; MatchFound = false})
    

let initModel() = {
    Cards = getCards()
    FirstSelection = None
    SecondSelection = None
}
// Update

let cardsEqual id1 id2 (cards: Card list) =
    let card1 = cards |> List.find (fun c -> c.Id = id1)
    let card2 = cards |> List.find (fun c -> c.Id = id2)
    card1.ImgUrl = card2.ImgUrl


let cardSelected id (cards: Card list) = 
    let card = List.find (fun c -> c.Id = id) cards
    card.Selected

let gameCleared (model: Model) =  
    List.forall (fun card -> card.MatchFound) model.Cards

let rec update model action =
    match action with
    | StartNewGame -> initModel()
    | SelectCard index -> 
        match model.FirstSelection, model.SecondSelection with
        | None, None -> 
            let cards = 
                model.Cards 
                |> List.map (fun card ->
                     if card.Id = index 
                     then { card with Selected = true }
                     else card
                )
            { model with Cards = cards; FirstSelection = Some index }
        | Some id, None when id = index -> model
        | Some id, None when cardsEqual id index (model.Cards) ->
            let cards =
                model.Cards              
                |> List.map (fun card ->
                     if card.Id = index || card.Id = id
                     then { card with Selected = true; MatchFound = true }
                     else card
                )
            { model with Cards = cards; FirstSelection = None; SecondSelection = None } 
        | Some id, None when id <> index ->  
            let cards =
                 model.Cards   
                 |> List.map (fun card ->
                      if card.Id = index || card.Id = id
                      then { card with Selected = true }
                      else card
                 )
            { model with Cards = cards; SecondSelection = Some index } 
        | Some id, Some id' when cardsEqual id' index (model.Cards) -> 
            let cards = 
                model.Cards 
                 |> List.map (fun card ->
                      if (card.Id = id && not card.MatchFound) 
                      then { card with Selected = false }
                      elif (card.Id = id' || card.Id = index) 
                      then { card with Selected = true; MatchFound = true }
                      else card
                 )
            let model' = { model with Cards = cards; FirstSelection = None; SecondSelection = None }
            update model' action
        | Some id, Some id' -> 
            let cards = 
                model.Cards 
                 |> List.map (fun card ->
                      if (card.Id = id || card.Id = id') && not card.MatchFound
                      then { card with Selected = false }
                      elif card.Id = index 
                      then { card with Selected = true }
                      else card
                 )
            let model' = { model with Cards = cards; FirstSelection = None; SecondSelection = None }
            update model' action
        | _, _ -> failwith "Cannot happen :)"
    | NoOp -> model


let viewCard (card: Card) = 
    div [
            classList ["card-container", true; "match-found", card.MatchFound] 
            onMouseClick (fun _ -> if not (card.MatchFound) && not (card.Selected) then SelectCard card.Id else NoOp)
        ] 
        [
            img [ 
                attribute "src" (if card.Selected then card.ImgUrl else "images/fable.png") 
            ]
        ]

let view model =
    if gameCleared model 
    then 
        div [] [
                h1 [] [ 
                    text "Congrats, You win!"
                    button [onMouseClick (fun _ -> StartNewGame) ] [text "Start new game"] 
                ]; 
            ]
    else
        div [ Style ["width", "500px"] ] 
            [ for card in model.Cards -> viewCard card ]
        
let initialModel = initModel()
createSimpleApp initialModel view update Virtualdom.createRender
|> withStartNodeSelector "#main"
|> start