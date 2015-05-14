# C# - Menu Driven Interface
A library that has a Menu Driven Interface based approach, and is intended for C# console applications.

## Requirements

- **.NETFramework,Version=v4.5.**

## Usage

- You can download or clone the whole solution or just download the dll file and reference it in your project - [dll] (https://github.com/Morten158/mdi_api/raw/master/dll/mdi-api.dll)

## Note

- This library was built as a part of a C# assigment at school, in hindsight a lot of things could have been done
differently. Also, sometimes when the program is exited by using the "cross" button it crashes, even tough everything is cleaned up before it actually exits.
Not a dealbreaker though.

## API Documentation

- I have relied on [Xml documentation] (https://msdn.microsoft.com/en-us/library/b2s063f7.aspx) comments to describe types and functions.

## Example
    using System;
    using mdi_api;
    using mdi_api.factories;
    using mdi_api.menus;
    using mdi_api.views.items;

    namespace Dummy {
        class Application : ConsoleApplication {

            private readonly Menu _menu;
            private readonly Item _addItem, _quit;
            private int _itemAdded = 0;

            public Application() {

                Console.Title = "My silly application";
            
                SetResolution(Resolution.DEFAULT);

                _menu = new Menu(Console.Title);
                _menu.Get(ViewFactory.CONTENT).VerticalSpacing = 2;

                _addItem = new Item("Add Item");
                _addItem.AddHotKeyListener(ConsoleKey.A, AddItem);
                _quit = new Item("Quit", Item.Alignment.LEFT);
                _quit.AddHotKeyListener(ConsoleKey.Q, Terminate);

                _menu.Get(ViewFactory.CONTENT).Add(_addItem);
                _menu.Get(ViewFactory.FOOTER).Add(_quit);
            }

            public void AddItem() {
                if(_itemAdded < 6)
                    _menu.Get(ViewFactory.CONTENT).Add(new Item("Item Added " + ++_itemAdded));
            }

            protected override void Terminate() {
                _addItem.RemoveHotKeyListener(AddItem);
                _quit.RemoveHotKeyListener(Terminate);
                base.Terminate();
            }

            static void Main(string[] args) {
                new Application();
            }
        }
    }

## Create custom menus

- Snake

![Snake] (https://github.com/Morten158/mdi_api/blob/master/sample/snake.png)

- Recipe Manager

![Recipe] (https://github.com/Morten158/mdi_api/blob/master/sample/recipe.png)

## Demo Projects

- If you want to explore further, download the demo projects: **mdi-snake** | **mdi-basic-demo**

## Want to Contribute?

- Fork and clone locally.
- Create a topic specific branch. Add some nice feature.

## License

The MIT license (Refer to the [LICENSE.md][license] file)

 [license]: https://github.com/Morten158/mdi_api/blob/master/LICENSE.md
