# C# - Menu Driven Interface
A library that has a Menu Driven Interface based approach, and can be used in a C# console application.

## Requirements

- **Windows:** .NETFramework,Version=v4.5.

## Note

-This library was built as a part of a C# assigment at school, in hindsight a lot of things could have been done
differently.

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

                Console.Title = "My stupid application";
            
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

## Create custom views

-Snake

![Snake] (https://github.com/Morten158/mdi_api/blob/master/sample/snake.png)

## Want to Contribute?

- Fork and clone locally.
- Create a topic specific branch. Add some nice feature.

## License

The MIT license (Refer to the [LICENSE.md][license] file)

 [license]: https://github.com/Morten158/mdi_api/blob/master/LICENSE.md
