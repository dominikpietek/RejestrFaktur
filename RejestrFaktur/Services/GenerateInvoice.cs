using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using RejestrFaktur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RejestrFaktur.Services
{
    public class GenerateInvoice
    {
        private byte[] _pdfDocument;
        private readonly InvoiceModel _model;
        private double _finalPrice = 0;
        private Document _generatedDocument;

        public GenerateInvoice(InvoiceModel model)
        {
            _model = model;
        }
        
        public void Generate()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            _generatedDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().Element(ComposeSigns);
                });
            });

            _pdfDocument = _generatedDocument.GeneratePdf();
        }

        public void Save(string fileName)
        {
            ConvertPdf.FromByteArray(_pdfDocument, fileName);
        }

        public void Show()
        {
            if(_generatedDocument != null)
            {
                _generatedDocument.GeneratePdfAndShow();
            }
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    string typeOfInvoice = (int)_model.InvoiceType == 3 ? "VAT Marża" : _model.InvoiceType.ToString();
                    column
                        .Item().Text($"Faktura {typeOfInvoice} Nr. {_model.InvoiceNumber}")
                        .FontSize(20).SemiBold();

                    column.Item().Text(text =>
                    {
                        text.Span("Data Wystawienia: ").SemiBold();
                        text.Span($"{_model.InvoiceDate.ToShortDateString()}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Data Sprzedaży: ").SemiBold();
                        text.Span($"{_model.InvoiceSellDate.ToShortDateString()}");
                    });
                    column.Item().Text(text =>
                    {
                        text.Span("Sposób Płatności: ").SemiBold();
                        string payment = (int)_model.PaymentMethod == 0 ? "Przelew Bankowy" : _model.PaymentMethod.ToString();
                        text.Span($"{payment}");
                    });
                    column.Item().Text(text =>
                    {
                        text.Span("Numer Konta: ").SemiBold();
                        text.Span($"{_model.BankAccountNumber}");
                    });
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(20);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Spacing(2);

                        column.Item().Text("Sprzedawca").SemiBold();
                        column.Item().PaddingBottom(5).LineHorizontal(1);

                        column.Item().DefaultTextStyle(x => x.FontSize(10)).Text($"{_model.Owner.Name}");
                        column.Item().DefaultTextStyle(x => x.FontSize(10)).Text($"{_model.Owner.Address}");
                        column.Item().DefaultTextStyle(x => x.FontSize(10)).Text($"NIP: {_model.Owner.NIPNumber}");
                    });
                    row.ConstantItem(50);
                    row.RelativeItem().Column(column =>
                    {
                        column.Spacing(2);

                        column.Item().Text("Nabywca").SemiBold();
                        column.Item().PaddingBottom(5).LineHorizontal(1);

                        column.Item().DefaultTextStyle(x => x.FontSize(10)).Text($"{_model.Contractor.Name}");
                        column.Item().DefaultTextStyle(x => x.FontSize(10)).Text($"{_model.Contractor.Address}");
                        column.Item().DefaultTextStyle(x => x.FontSize(10)).Text($"NIP: {_model.Contractor.NIPNumber}");
                    });
                });
                column.Item().Element(ComposeTable);
                column.Item().Element(ComposeSum);
            });
        }

        private void ComposeTable(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(50);
                    columns.RelativeColumn();
                    columns.ConstantColumn(50);
                    columns.ConstantColumn(50);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Height(40).Border(1).BorderColor(Colors.Black).AlignCenter().Text("Lp.");
                    header.Cell().Height(40).Border(1).BorderColor(Colors.Black).AlignCenter().Text("Nazwa").Style(headerStyle);
                    header.Cell().Height(40).Border(1).BorderColor(Colors.Black).AlignCenter().Text("Miara").Style(headerStyle);
                    header.Cell().Height(40).Border(1).BorderColor(Colors.Black).AlignCenter().Text("Ilość").Style(headerStyle);
                    header.Cell().Height(40).Border(1).BorderColor(Colors.Black).AlignCenter().Text("Cena netto").Style(headerStyle);
                    header.Cell().Height(40).Border(1).BorderColor(Colors.Black).AlignCenter().Text("Wartość netto").Style(headerStyle);
                    header.Cell().Height(40).Border(1).BorderColor(Colors.Black).AlignCenter().Text("Stawka VAT").Style(headerStyle);
                    header.Cell().Height(40).Border(1).BorderColor(Colors.Black).AlignCenter().Text("Kwota VAT").Style(headerStyle);
                    header.Cell().Height(40).Border(1).BorderRight(1).BorderColor(Colors.Black).AlignCenter().Text("Wartość brutto").Style(headerStyle);
                });

                int lp = 0;
                double netSum = 0;
                double taxSum = 0;
                _finalPrice = 0;
                foreach (var item in _model.Items)
                {
                    lp += 1;
                    double net = item.NetPrice * item.Amount;
                    double tax = net * (item.TaxRate / 1000);
                    double gross = net + tax;
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{lp}");
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{item.Name}");
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{item.Unit}");
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{item.Amount}");
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{item.NetPrice}");
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{Math.Round(net, 2)}");
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{Math.Round((double)(item.TaxRate / 10), 2)}%");
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{Math.Round(tax, 2)}");
                    table.Cell().Border(1).BorderColor(Colors.Black).DefaultTextStyle(x => x.FontSize(10)).AlignCenter().Text($"{Math.Round(gross, 2)}");
                    netSum += net;
                    taxSum += tax;
                    _finalPrice += gross;
                }

                table.Cell().Text("");
                table.Cell().Text("");
                table.Cell().Text("");
                table.Cell().Text("");
                table.Cell().Text("");
                table.Cell().DefaultTextStyle(x => x.FontSize(10)).Border(1).BorderColor(Colors.Black).AlignCenter().Text($"{Math.Round(netSum, 2)}");
                table.Cell().DefaultTextStyle(x => x.FontSize(10)).Border(1).BorderColor(Colors.Black).AlignCenter().Text("-");
                table.Cell().DefaultTextStyle(x => x.FontSize(10)).Border(1).BorderColor(Colors.Black).AlignCenter().Text($"{Math.Round(taxSum, 2)}");
                table.Cell().DefaultTextStyle(x => x.FontSize(10)).Border(1).BorderColor(Colors.Black).AlignCenter().Text($"{Math.Round(_finalPrice, 2)}");
            });
        }

        private void ComposeSum(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(130);
                    columns.RelativeColumn();
                });
                table.Cell().Text("Pozostało do zapłaty:");
                table.Cell().Text($"{Math.Round(_finalPrice, 2)} zł");
                table.Cell().Text("Słownie:");
                var changeToWords = new ChangeNumbersToWords(_finalPrice);
                table.Cell().Text($"{changeToWords.ToWords()[1..]}");
            });
        }

        private void ComposeSigns(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Item().AlignMiddle().Text($"Opis: {_model.Description}");
                column.Item().Height(70);
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Spacing(2);

                        column.Item().PaddingBottom(5).LineHorizontal(1);

                        column.Item().Text("podpis sprzedawcy");
                    });
                    row.ConstantItem(170);
                    row.RelativeItem().Column(column =>
                    {
                        column.Spacing(2);

                        column.Item().PaddingBottom(5).LineHorizontal(1);

                        column.Item().Text("podpis nabywcy");
                    });
                });
            });
        }
    }
}
