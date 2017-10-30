using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsAppEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public async void OnOpen()
        {
            try
            {
                var picker = new FileOpenPicker()
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.DocumentsLibrary
                };
                picker.FileTypeFilter.Add(".txt");
                picker.FileTypeFilter.Add(".md");

                StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    IRandomAccessStreamWithContentType stream = await file.OpenReadAsync();
                    using (var reader = new DataReader(stream))
                    {
                        await reader.LoadAsync((uint)stream.Size);

                        text1.Text = reader.ReadString((uint)stream.Size);
                    }
                }
            }
            catch (Exception ex)
            {
                var dlg = new MessageDialog(ex.Message, "Error");
                await dlg.ShowAsync();
            }
        }

        public async void OnOpenDotnet()
        {
            try
            {
                var picker = new FileOpenPicker()
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.DocumentsLibrary
                };
                picker.FileTypeFilter.Add(".txt");
                picker.FileTypeFilter.Add(".md");

                StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    IRandomAccessStreamWithContentType wrtStream = await file.OpenReadAsync();
                    Stream stream = wrtStream.AsStreamForRead();
                    using (var reader = new StreamReader(stream))
                    {
                        text1.Text = await reader.ReadToEndAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var dlg = new MessageDialog(ex.Message, "Error");
                await dlg.ShowAsync();
            }
        }

        public async void OnSaveDotnet()
        {
            try
            {
                var picker = new FileSavePicker()
                {
                    SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                    SuggestedFileName = "New Document"
                };
                picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

                StorageFile file = await picker.PickSaveFileAsync();
                if (file != null)
                {
                    using (StorageStreamTransaction tx = await file.OpenTransactedWriteAsync())
                    {
                        Stream stream = tx.Stream.AsStreamForWrite();
                        using (var writer = new StreamWriter(stream))
                        {
                            byte[] preamble = Encoding.UTF8.GetPreamble();
                            await stream.WriteAsync(preamble, 0, preamble.Length);
                            await writer.WriteAsync(text1.Text);
                            await writer.FlushAsync();
                            tx.Stream.Size = (ulong)stream.Length;
                            await tx.CommitAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var dlg = new MessageDialog(ex.Message, "Error");
                await dlg.ShowAsync();
            }
        }

        public async void OnSave()
        {
            try
            {
                var picker = new FileSavePicker()
                {
                    SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                    SuggestedFileName = "New Document"
                };
                picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

                StorageFile file = await picker.PickSaveFileAsync();
                if (file != null)
                {
                    using (StorageStreamTransaction tx = await file.OpenTransactedWriteAsync())
                    {
                        IRandomAccessStream stream = tx.Stream;
                        stream.Seek(0);
                        using (var writer = new DataWriter(stream))
                        {
                            writer.WriteString(text1.Text);
                            tx.Stream.Size = await writer.StoreAsync();

                            await tx.CommitAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var dlg = new MessageDialog(ex.Message, "Error");
                await dlg.ShowAsync();
            }
        }
    }
}
