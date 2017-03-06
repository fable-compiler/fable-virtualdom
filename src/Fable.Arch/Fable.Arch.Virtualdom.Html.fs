module Fable.Arch.Virtualdom.Html

open Fable.Core

[<Erase>]
type Value = 
    | Str of string
    | Int of int
    | Float of float


// TODO: Refinement for value types -> remove all "of obj" to their respective type

[<KeyValueList>]
type VAttr = 
    | Style of string
    | ClassName of string
    | Id of string
    | BoxFlex of int
    | BoxFlexGroup of int
    | ColumnCount of int
    | Flex of Value
    | FlexGrow of float
    | FlexShrink of float
    | FontWeight of Value
    | LineClamp of float
    | LineHeight of Value
    | Opacity of float
    | Order of float
    | Orphans of float
    | Widows of float
    | ZIndex of int
    | Zoom of float
    | FontSize of Value
    | FillOpacity of float
    | StrokeOpacity of float
    | StrokeWidth of float
    | AlignContent of obj
    | AlignItems of obj
    | AlignSelf of obj
    | AlignmentAdjust of obj
    | AlignmentBaseline of obj
    | AnimationDelay of obj
    | AnimationDirection of obj
    | AnimationIterationCount of obj
    | AnimationName of obj
    | AnimationPlayState of obj
    | Appearance of obj
    | BackfaceVisibility of obj
    | BackgroundBlendMode of obj
    | BackgroundColor of obj
    | BackgroundComposite of obj
    | BackgroundImage of obj
    | BackgroundOrigin of obj
    | BackgroundPositionX of obj
    | BackgroundRepeat of obj
    | BaselineShift of obj
    | Behavior of obj
    | Border of obj
    | BorderBottomLeftRadius of obj
    | BorderBottomRightRadius of obj
    | BorderBottomWidth of obj
    | BorderCollapse of obj
    | BorderColor of obj
    | BorderCornerShape of obj
    | BorderImageSource of obj
    | BorderImageWidth of obj
    | BorderLeft of obj
    | BorderLeftColor of obj
    | BorderLeftStyle of obj
    | BorderLeftWidth of obj
    | BorderRight of obj
    | BorderRightColor of obj
    | BorderRightStyle of obj
    | BorderRightWidth of obj
    | BorderSpacing of obj
    | BorderStyle of obj
    | BorderTop of obj
    | BorderTopColor of obj
    | BorderTopLeftRadius of obj
    | BorderTopRightRadius of obj
    | BorderTopStyle of obj
    | BorderTopWidth of Value
    | BorderWidth of Value
    | Bottom of obj
    | BoxAlign of obj
    | BoxDecorationBreak of obj
    | BoxDirection of obj
    | BoxLineProgression of obj
    | BoxLines of obj
    | BoxOrdinalGroup of obj
    | BreakAfter of obj
    | BreakBefore of obj
    | BreakInside of obj
    | Clear of obj
    | Clip of obj
    | ClipRule of obj
    | Color of obj
    | ColumnFill of obj
    | ColumnGap of obj
    | ColumnRule of obj
    | ColumnRuleColor of obj
    | ColumnRuleWidth of obj
    | ColumnSpan of obj
    | ColumnWidth of obj
    | Columns of obj
    | CounterIncrement of obj
    | CounterReset of obj
    | Cue of obj
    | CueAfter of obj
    | Direction of obj
    | Display of obj
    | Fill of obj
    | FillRule of obj
    | Filter of obj
    | FlexAlign of obj
    | FlexBasis of obj
    | FlexDirection of obj
    | FlexFlow of obj
    | FlexItemAlign of obj
    | FlexLinePack of obj
    | FlexOrder of obj
    | FlexWrap of obj
    | Float of obj
    | FlowFrom of obj
    | Font of obj
    | FontFamily of obj
    | FontKerning of obj
    | FontSizeAdjust of obj
    | FontStretch of obj
    | FontStyle of obj
    | FontSynthesis of obj
    | FontVariant of obj
    | FontVariantAlternates of obj
    | GridArea of obj
    | GridColumn of obj
    | GridColumnEnd of obj
    | GridColumnStart of obj
    | GridRow of obj
    | GridRowEnd of obj
    | GridRowPosition of obj
    | GridRowSpan of obj
    | GridTemplateAreas of obj
    | GridTemplateColumns of obj
    | GridTemplateRows of obj
    | Height of obj
    | HyphenateLimitChars of obj
    | HyphenateLimitLines of obj
    | HyphenateLimitZone of obj
    | Hyphens of obj
    | ImeMode of obj
    | JustifyContent of obj
    | LayoutGrid of obj
    | LayoutGridChar of obj
    | LayoutGridLine of obj
    | LayoutGridMode of obj
    | LayoutGridType of obj
    | Left of obj
    | LetterSpacing of obj
    | LineBreak of obj
    | ListStyle of obj
    | ListStyleImage of obj
    | ListStylePosition of obj
    | ListStyleType of obj
    | Margin of obj
    | MarginBottom of obj
    | MarginLeft of obj
    | MarginRight of obj
    | MarginTop of obj
    | MarqueeDirection of obj
    | MarqueeStyle of obj
    | Mask of obj
    | MaskBorder of obj
    | MaskBorderRepeat of obj
    | MaskBorderSlice of obj
    | MaskBorderSource of obj
    | MaskBorderWidth of obj
    | MaskClip of obj
    | MaskOrigin of obj
    | MaxFontSize of obj
    | MaxHeight of obj
    | MaxWidth of obj
    | MinHeight of obj
    | MinWidth of obj
    | Outline of obj
    | OutlineColor of obj
    | OutlineOffset of obj
    | Overflow of obj
    | OverflowStyle of obj
    | OverflowX of obj
    | Padding of obj
    | PaddingBottom of obj
    | PaddingLeft of obj
    | PaddingRight of obj
    | PaddingTop of obj
    | PageBreakAfter of obj
    | PageBreakBefore of obj
    | PageBreakInside of obj
    | Pause of obj
    | PauseAfter of obj
    | PauseBefore of obj
    | Perspective of obj
    | PerspectiveOrigin of obj
    | PointerEvents of obj
    | Position of obj
    | PunctuationTrim of obj
    | Quotes of obj
    | RegionFragment of obj
    | RestAfter of obj
    | RestBefore of obj
    | Right of obj
    | RubyAlign of obj
    | RubyPosition of obj
    | ShapeImageThreshold of obj
    | ShapeInside of obj
    | ShapeMargin of obj
    | ShapeOutside of obj
    | Speak of obj
    | SpeakAs of obj
    | TabSize of obj
    | TableLayout of obj
    | TextAlign of obj
    | TextAlignLast of obj
    | TextDecoration of obj
    | TextDecorationColor of obj
    | TextDecorationLine of obj
    | TextDecorationLineThrough of obj
    | TextDecorationNone of obj
    | TextDecorationOverline of obj
    | TextDecorationSkip of obj
    | TextDecorationStyle of obj
    | TextDecorationUnderline of obj
    | TextEmphasis of obj
    | TextEmphasisColor of obj
    | TextEmphasisStyle of obj
    | TextHeight of obj
    | TextIndent of obj
    | TextJustifyTrim of obj
    | TextKashidaSpace of obj
    | TextLineThrough of obj
    | TextLineThroughColor of obj
    | TextLineThroughMode of obj
    | TextLineThroughStyle of obj
    | TextLineThroughWidth of obj
    | TextOverflow of obj
    | TextOverline of obj
    | TextOverlineColor of obj
    | TextOverlineMode of obj
    | TextOverlineStyle of obj
    | TextOverlineWidth of obj
    | TextRendering of obj
    | TextScript of obj
    | TextShadow of obj
    | TextTransform of obj
    | TextUnderlinePosition of obj
    | TextUnderlineStyle of obj
    | Top of obj
    | TouchAction of obj
    | Transform of obj
    | TransformOrigin of obj
    | TransformOriginZ of obj
    | TransformStyle of obj
    | Transition of obj
    | TransitionDelay of obj
    | TransitionDuration of obj
    | TransitionProperty of obj
    | TransitionTimingFunction of obj
    | UnicodeBidi of obj
    | UnicodeRange of obj
    | UserFocus of obj
    | UserInput of obj
    | VerticalAlign of obj
    | Visibility of obj
    | VoiceBalance of obj
    | VoiceDuration of obj
    | VoiceFamily of obj
    | VoicePitch of obj
    | VoiceRange of obj
    | VoiceRate of obj
    | VoiceStress of obj
    | VoiceVolume of obj
    | WhiteSpace of obj
    | WhiteSpaceTreatment of obj
    | Width of obj
    | WordBreak of obj
    | WordSpacing of obj
    | WordWrap of obj
    | WrapFlow of obj
    | WrapMargin of obj
    | WrapOption of obj
    | WritingMode of obj
    | [<Erase>] VAttr of string * Value
    
[<KeyValueList>]
type VProps<'T> = 
    | [<CompiledName("attributes")>] Attrs of VAttr list
    | Key of string
    | Alt of string
    | Src of string
    | Href of string
    | Width of Value
    | Height of Value
    | Value of string
    | Placeholder of string
    | Disabled of bool
    | EncType of string
    | Form of string
    | FormAction of string
    | FormEncType of string
    | FormMethod of string
    | FormNoValidate of bool
    | FormTarget of string
    | Default of bool
    | Defer of bool
    | Cols of int
    | ColSpan of int
    | Checked of bool
    | CharSet of string
    | CellPadding of int
    | CellSpacing of int
    | Async of bool
    | AutoComplete of string
    | AutoFocus of bool
    | AutoPlay of bool
    | Capture of bool
    | DefaultChecked of bool
    | DefaultValue of U2<string, ResizeArray<string>>
    | Accept of string
    | AcceptCharset of string
    | AccessKey of string
    | Action of string
    | AllowFullScreen of bool
    | AllowTransparency of bool
    | [<Erase>] VProp of string * Value
    | [<CompiledName("ev-click")>] OnClick of (obj -> 'T)


type VTag = string



type VNode<'T> = 
    | Plain of string
    | Unary of VTag * VProps<'T> list
    | Nested of VTag * VProps<'T> list * VNode<'T> list


/// Example usage
let internal text content = Plain content 
let internal div props children = Nested ("div", props, children)

type internal Message = Incr | Decr

let internal view : VNode<Message> = 
      div 
        [
           Attrs [
              Style "color:white"
              ClassName "content"
              Id "unique"
           ]
           OnClick (fun ev -> Decr)
           
        ] 
        [
          div [] [ text "First div"  ]
          div [] [ text "Second div" ]
          div [] [ text "Third div"  ]
        ]