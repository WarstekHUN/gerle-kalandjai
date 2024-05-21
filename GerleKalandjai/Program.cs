﻿using Gerle_Lib.BaseClasses;
using MenuSystem;
using Spectre.Console;

class Program
{
    private static Menu mainMenu;
    private static Action<string> WriteAction;

    private static BeautyWriter bw = new BeautyWriter();

    /// <summary>
    /// A program belépési pontja.
    /// </summary>
    /// <param name="args">A parancssori argumentumok.</param>
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        WriteAction = text => bw.Write(text);

        Menu.SetCreator("Tatár Mátyás Bence, Kluitenberg Alex, Gáspár Mihály, Balogh Levente");

        mainMenu = new Menu(new[] { "Játék 📁", "Beállítások 📝", "Kilépés 🚪" }, new Action[] {
            GameMenu,
            SettingsMenu,
            Exit
        });
    }

    /// <summary>
    /// A játék menü megjelenítése.
    /// </summary>
    static void GameMenu()
    {
        Menu sm = new Menu(new string[] { "Meglévő folytatása 🆕", "Új játék 📂" }, new Action[] {
            () => WriteAction("[bold yellow on blue]Meglévő folytatása![/] :globe_showing_europe_africa:"),
            NewGameMenu
        }, true, mainMenu);
    }

    /// <summary>
    /// Az új játék menü megjelenítése.
    /// </summary>
    static void NewGameMenu()
    {
        Menu sm = new Menu(new string[] { "Nehézség ✂️", "Játék neve🔤" }, new Action[] {
            () => WriteAction("[bold yellow on blue]Nehézség![/] :globe_showing_europe_africa:"),
            () => WriteAction("[bold yellow on blue]Játék neve![/] :globe_showing_europe_africa:")
        }, true, mainMenu);
    }

    /// <summary>
    /// A beállítások menü megjelenítése.
    /// </summary>
    static void SettingsMenu()
    {
        Menu sm = new Menu(new string[] { "Hang 📋" }, new Action[] { SoundSettingsMenu }, true, mainMenu);
    }

    /// <summary>
    /// A hangbeállítások menü megjelenítése.
    /// </summary>
    static void SoundSettingsMenu()
    {
        Menu sm = new Menu(new string[] { "Zene hangereje🎵", "Szinkron hangereje 🗣️" }, new Action[] {
            () => WriteAction("[bold yellow on blue]Zene hangereje![/] :globe_showing_europe_africa:"),
            () => WriteAction("[bold yellow on blue]Szinkron hangereje![/] :globe_showing_europe_africa:")
        }, true, mainMenu);
    }

    /// <summary>
    /// Kilép a programból.
    /// </summary>
    static void Exit()
    {
        WriteAction("[bold yellow on blue]Kilépés![/] :globe_showing_europe_africa:");
    }
}
