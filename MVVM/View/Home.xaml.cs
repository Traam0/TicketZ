using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TicketZ.MVVM.Model;

namespace TicketZ.MVVM.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            if (sender is not Button) return;

            if (((Button)sender).DataContext is Shift shift)
            {
              

                byte[] pdf = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(80, 210, QuestPDF.Infrastructure.Unit.Millimetre);
                        page.MarginVertical(1, Unit.Centimetre);

                        page.PageColor(Colors.White);

                        page.Content()
                            .PaddingHorizontal(1, Unit.Centimetre)
                            .AlignCenter()
                            .Column(column =>
                            {
                                column.Spacing(15);

                                column
                                    .Item()
                                    .Column(x =>
                                    {
                                        x.Item().AlignCenter().Text("Ticket Z").Bold().FontSize(20);
                                        x.Item()
                                            .AlignCenter()
                                            .Text("Parking " + "Friends Equipement")
                                            .FontSize(12)
                                            .SemiBold();
                                        x.Item()
                                            .PaddingVertical(8)
                                            .Table(table =>
                                            {
                                                table.ColumnsDefinition(columns =>
                                                {
                                                    columns.RelativeColumn();
                                                    columns.RelativeColumn();
                                                });

                                                table
                                                    .Cell()
                                                    .Row(1)
                                                    .Column(1)
                                                    .Element(CellStyle)
                                                    .PaddingVertical(2)
                                                    .PaddingHorizontal(5)
                                                    .Text("Date:");
                                                table
                                                    .Cell()
                                                    .Row(2)
                                                    .Column(1)
                                                    .Element(CellStyle)
                                                    .PaddingVertical(2)
                                                    .PaddingHorizontal(5)
                                                    .Text("Heure:");

                                                table
                                                    .Cell()
                                                    .Row(1)
                                                    .Column(2)
                                                    .Element(CellStyle)
                                                    .PaddingVertical(2)
                                                    .PaddingHorizontal(5)
                                                    .Text(DateTime.Now.ToString("dd-MM-yyyy"));
                                                table
                                                    .Cell()
                                                    .Row(2)
                                                    .Column(2)
                                                    .Element(CellStyle)
                                                    .PaddingVertical(2)
                                                    .PaddingHorizontal(5)
                                                    .Text(DateTime.Now.ToString("hh:mm"));

                                                static IContainer CellStyle(IContainer container)
                                                {
                                                    return container.DefaultTextStyle(
                                                        x =>
                                                            x.SemiBold()
                                                                .FontColor(Colors.Black)
                                                                .FontSize(9)
                                                    );
                                                }
                                            });
                                    });

                                column
                                    .Item()
                                    .Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn();
                                            columns.RelativeColumn();
                                        });

                                        table
                                            .Cell()
                                            .Row(1)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("Caissier");
                                        table
                                            .Cell()
                                            .Row(2)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("Debut session");
                                        table
                                            .Cell()
                                            .Row(3)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("Fin session");
                                        static IContainer CellStyle(IContainer container)
                                        {
                                            return container
                                                .DefaultTextStyle(
                                                    x =>
                                                        x.SemiBold()
                                                            .FontColor(Colors.Black)
                                                            .FontSize(11)
                                                )
                                                .BorderBottom(1)
                                                .BorderColor(Colors.Grey.Lighten1);
                                        }

                                        table
                                            .Cell()
                                            .Row(1)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(shift.Cashier);
                                        table
                                            .Cell()
                                            .Row(2)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(shift.StartTime.ToString("dd-MM-yyyy HH:mm"));
                                        table
                                            .Cell()
                                            .Row(3)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(DateTime.Now.ToString("dd-MM-yyyy HH:mm"));

                                        static IContainer CellStyleData(IContainer container)
                                        {
                                            return container
                                                .DefaultTextStyle(x => x.SemiBold().FontSize(9))
                                                .BorderBottom(1)
                                                .BorderColor(Colors.Grey.Lighten1)
                                                .Padding(1);
                                        }
                                    });

                                // ==============

                                column
                                    .Item()
                                    .Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn();
                                            columns.RelativeColumn();
                                        });

                                        table
                                            .Cell()
                                            .Row(1)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("Avances");

                                        table
                                            .Cell()
                                            .Row(1)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(shift.AdvanceAmount.ToString() + " Dh");

                                        // ---------------------
                                        table
                                            .Cell()
                                            .Row(2)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("montant encaisse");

                                        table
                                            .Cell()
                                            .Row(2)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(shift.AmountReceived.ToString() + " Dh");

                                        // ---------------------

                                        table
                                            .Cell()
                                            .Row(3)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("Temps libre");

                                        table
                                            .Cell()
                                            .Row(3)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(shift.FreeTime.ToString() + " h");

                                        // ---------------------

                                        table
                                            .Cell()
                                            .Row(4)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("Totale");

                                        table
                                            .Cell()
                                            .Row(4)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(shift.Total + " Dhs");

                                        // ---------------------

                                        table
                                            .Cell()
                                            .Row(5)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("Ouverture manuelle");

                                        table
                                            .Cell()
                                            .Row(5)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(shift.ManualOpenings.ToString());
                                                                              // ---------------------

                                        table
                                            .Cell()
                                            .Row(6)
                                            .Column(1)
                                            .Element(CellStyle)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text("véhicules garés");

                                        table
                                            .Cell()
                                            .Row(6)
                                            .Column(2)
                                            .Element(CellStyleData)
                                            .PaddingVertical(2)
                                            .PaddingHorizontal(5)
                                            .Text(shift.ParkedVehicules.ToString());

                                        static IContainer CellStyle(IContainer container)
                                        {
                                            return container
                                                .DefaultTextStyle(x => x.SemiBold().FontSize(9))
                                                .BorderBottom(1)
                                                .BorderColor(Colors.Grey.Medium)
                                                .PaddingHorizontal(2)
                                                .PaddingVertical(5);
                                        }

                                        static IContainer CellStyleData(IContainer container)
                                        {
                                            return container
                                                .DefaultTextStyle(x => x.Medium().FontSize(9))
                                                .BorderBottom(1)
                                                .BorderColor(Colors.Grey.Medium)
                                                .PaddingHorizontal(2)
                                                .PaddingVertical(5);
                                        }
                                    });
                            });
                    });
                }).GeneratePdf();

                

                using (
                  FileStream fs =
                      new(
                          Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TicketZ", "umm.pdf"),
                          FileMode.Create,
                          FileAccess.Write
                      )
              )
                {
                    fs.Write(pdf);
                }
            }
        }
    }
    
}
