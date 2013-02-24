[<AutoOpen>]
module Squares
open IntelliFactory.WebSharper.Formlet
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.JQuery
open IntelliFactory.WebSharper.Raphael

type SquareControl() =
  inherit Web.Control()

  [<JavaScript>]
  let gen() =
    let id = "paper"

    Div[Attr.Id id] |>! OnAfterRender (fun el ->
      let paper = Raphael(id, 300., 300.)
      
      let path1 = Raphael.Path().Absolute.MoveTo(0.,0.).LineTo(0.,100.).LineTo(100.,100.).LineTo(100.,0.).LineTo(0.,0.)
      let path2 = Raphael.Path().Absolute.MoveTo(100.,100.).LineTo(100.,200.).LineTo(200.,200.).LineTo(200.,100.).LineTo(100.,100.)
      
      let path1' = paper.Path(path1).Fill("#CCC")
      let path2' = paper.Path(path2).Fill("#CCC")    
      
      // Fails
      for p in [path1';path2'] do
        p.Click(fun() -> p.Fill("#000") |> ignore) |> ignore
    )

    // Works
    //path1'.Click(fun() -> path1'.Fill("#000") |> ignore) |> ignore
    //path2'.Click(fun() -> path2'.Fill("#000") |> ignore) |> ignore

  [<JavaScript>]
  override this.Body =
    upcast gen()
