namespace HtmlSite1

open System
open System.IO
open System.Web
open IntelliFactory.WebSharper.Sitelets

/// Defines a sample HTML site with nested pages
module SampleSite =
    open IntelliFactory.WebSharper
    open IntelliFactory.Html

    // Action type
    type Action =
        | Index

    module Skin =

        type Page =
            {
                Title : string
                Body : list<Content.HtmlElement>
            }

        let MainTemplate =
            let path = Path.Combine(__SOURCE_DIRECTORY__, "Main.html")
            Content.Template<Page>(path)
                .With("title", fun x -> x.Title)
                .With("body", fun x -> x.Body)

        let WithTemplate title body : Content<Action> =
            Content.WithTemplate MainTemplate <| fun context ->
                {
                    Title = title
                    Body = body context
                }

    // Module containing client-side controls
    module Client =
        open IntelliFactory.WebSharper.Html

        type MyControl() =
            inherit IntelliFactory.WebSharper.Web.Control ()

            [<JavaScript>]
            override this.Body =
                I [Text "Client control"] :> IPagelet

    let Index =
        Skin.WithTemplate "Index page" <| fun ctx ->
            [
                P [Text "Clicking on a square should change its colour"]
                Div [new SquareControl()]
            ]

    let MySitelet = Sitelet.Content "/index" Action.Index Index

    // Actions to generate pages from
    let MyActions =
        [
            Action.Index
        ]

/// The class that contains the website
type MySampleWebsite() =
    interface IWebsite<SampleSite.Action> with
        member this.Sitelet = SampleSite.MySitelet
        member this.Actions = SampleSite.MyActions

[<assembly: WebsiteAttribute(typeof<MySampleWebsite>)>]
do ()
