using System.Windows.Input;


namespace BooksDemo.Commands
{
  public static class BooksCommands
  {
    private static RoutedUICommand s_showBook;
    public static ICommand ShowBook =>
        s_showBook ?? (s_showBook = new RoutedUICommand("Show Book", "ShowBook", typeof(BooksCommands)));


    private static RoutedUICommand s_showBooksList;
    public static ICommand ShowBooksList
    {
      get
      {
        if (s_showBooksList == null)
        {
          s_showBooksList = new RoutedUICommand("Show Books", "ShowBooks", typeof(BooksCommands));
          s_showBooksList.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Alt));
        }
        return s_showBooksList;
      }
    }

    private static RoutedUICommand s_showBooksGrid;
        public static ICommand ShowBooksGrid =>
            s_showBooksGrid ?? (s_showBooksGrid = new RoutedUICommand("Show Books Grid", "ShowBooksGrid", typeof(BooksCommands)));

    private static RoutedUICommand s_showAuthors;
    public static ICommand ShowAuthors =>
        s_showAuthors ?? (s_showAuthors = new RoutedUICommand("Show Authors", "ShowAuthors", typeof(BooksCommands)));
  }
}
