using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Xml;

namespace HeThongRapChieuPhim.Classes;

public static class InvoiceExcelExporter
{
    private const string SpreadsheetNamespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
    private const string RelationshipNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

    public static void Export(string filePath, IReadOnlyList<(string Label, string Value)> rows)
    {
        using var file = File.Create(filePath);
        using var archive = new ZipArchive(file, ZipArchiveMode.Create, leaveOpen: false, Encoding.UTF8);

        WriteXml(archive, "[Content_Types].xml", writer =>
        {
            writer.WriteStartDocument();
            const string contentTypesNamespace = "http://schemas.openxmlformats.org/package/2006/content-types";
            writer.WriteStartElement("Types", contentTypesNamespace);
            writer.WriteStartElement("Default", contentTypesNamespace);
            writer.WriteAttributeString("Extension", "rels");
            writer.WriteAttributeString("ContentType", "application/vnd.openxmlformats-package.relationships+xml");
            writer.WriteEndElement();
            writer.WriteStartElement("Default", contentTypesNamespace);
            writer.WriteAttributeString("Extension", "xml");
            writer.WriteAttributeString("ContentType", "application/xml");
            writer.WriteEndElement();
            writer.WriteStartElement("Override", contentTypesNamespace);
            writer.WriteAttributeString("PartName", "/xl/workbook.xml");
            writer.WriteAttributeString("ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml");
            writer.WriteEndElement();
            writer.WriteStartElement("Override", contentTypesNamespace);
            writer.WriteAttributeString("PartName", "/xl/worksheets/sheet1.xml");
            writer.WriteAttributeString("ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        });

        WriteXml(archive, "_rels/.rels", writer =>
        {
            writer.WriteStartDocument();
            const string relationshipsNamespace = "http://schemas.openxmlformats.org/package/2006/relationships";
            writer.WriteStartElement("Relationships", relationshipsNamespace);
            writer.WriteStartElement("Relationship", relationshipsNamespace);
            writer.WriteAttributeString("Id", "rId1");
            writer.WriteAttributeString("Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument");
            writer.WriteAttributeString("Target", "xl/workbook.xml");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        });

        WriteXml(archive, "xl/workbook.xml", writer =>
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("workbook", SpreadsheetNamespace);
            writer.WriteAttributeString("xmlns", "r", null, RelationshipNamespace);
            writer.WriteStartElement("sheets", SpreadsheetNamespace);
            writer.WriteStartElement("sheet", SpreadsheetNamespace);
            writer.WriteAttributeString("name", "Hóa đơn");
            writer.WriteAttributeString("sheetId", "1");
            writer.WriteAttributeString("r", "id", RelationshipNamespace, "rId1");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        });

        WriteXml(archive, "xl/_rels/workbook.xml.rels", writer =>
        {
            writer.WriteStartDocument();
            const string relationshipsNamespace = "http://schemas.openxmlformats.org/package/2006/relationships";
            writer.WriteStartElement("Relationships", relationshipsNamespace);
            writer.WriteStartElement("Relationship", relationshipsNamespace);
            writer.WriteAttributeString("Id", "rId1");
            writer.WriteAttributeString("Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet");
            writer.WriteAttributeString("Target", "worksheets/sheet1.xml");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        });

        WriteXml(archive, "xl/worksheets/sheet1.xml", writer => WriteWorksheet(writer, rows));
    }

    private static void WriteWorksheet(XmlWriter writer, IReadOnlyList<(string Label, string Value)> rows)
    {
        writer.WriteStartDocument();
        writer.WriteStartElement("worksheet", SpreadsheetNamespace);
        writer.WriteStartElement("cols", SpreadsheetNamespace);
        WriteColumn(writer, 1, 1, 30);
        WriteColumn(writer, 2, 2, 64);
        writer.WriteEndElement();
        writer.WriteStartElement("sheetData", SpreadsheetNamespace);

        for (int index = 0; index < rows.Count; index++)
        {
            int rowNumber = index + 1;
            writer.WriteStartElement("row", SpreadsheetNamespace);
            writer.WriteAttributeString("r", rowNumber.ToString(CultureInfo.InvariantCulture));
            WriteInlineStringCell(writer, $"A{rowNumber}", rows[index].Label);
            if (!string.IsNullOrEmpty(rows[index].Value))
                WriteInlineStringCell(writer, $"B{rowNumber}", rows[index].Value);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
        writer.WriteEndElement();
        writer.WriteEndDocument();
    }

    private static void WriteColumn(XmlWriter writer, int minimum, int maximum, int width)
    {
        writer.WriteStartElement("col", SpreadsheetNamespace);
        writer.WriteAttributeString("min", minimum.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("max", maximum.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("width", width.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("customWidth", "1");
        writer.WriteEndElement();
    }

    private static void WriteInlineStringCell(XmlWriter writer, string reference, string value)
    {
        writer.WriteStartElement("c", SpreadsheetNamespace);
        writer.WriteAttributeString("r", reference);
        writer.WriteAttributeString("t", "inlineStr");
        writer.WriteStartElement("is", SpreadsheetNamespace);
        writer.WriteStartElement("t", SpreadsheetNamespace);
        writer.WriteAttributeString("xml", "space", "http://www.w3.org/XML/1998/namespace", "preserve");
        writer.WriteString(value);
        writer.WriteEndElement();
        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    private static void WriteXml(ZipArchive archive, string path, Action<XmlWriter> write)
    {
        var entry = archive.CreateEntry(path, CompressionLevel.Optimal);
        using var stream = entry.Open();
        using var writer = XmlWriter.Create(stream, new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
            Indent = true,
            CloseOutput = false
        });
        write(writer);
    }
}
