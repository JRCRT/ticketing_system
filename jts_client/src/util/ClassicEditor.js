import { ClassicEditor as ClassicEditorBase } from "@ckeditor/ckeditor5-editor-classic";
import { Essentials } from "@ckeditor/ckeditor5-essentials";
import { Bold, Italic } from "@ckeditor/ckeditor5-basic-styles";
import { Paragraph } from "@ckeditor/ckeditor5-paragraph";
import { PasteFromOffice } from "@ckeditor/ckeditor5-paste-from-office";
import { Highlight } from "@ckeditor/ckeditor5-highlight";
import { Alignment } from "@ckeditor/ckeditor5-alignment";
import { DocumentList } from "@ckeditor/ckeditor5-list";
import {
  Table,
  TableToolbar,
  TableProperties,
  TableCellProperties,
  TableColumnResize,
} from "@ckeditor/ckeditor5-table";

export default class ClassicEditor extends ClassicEditorBase {}

ClassicEditor.builtinPlugins = [Essentials, Bold, Italic, Paragraph];

ClassicEditor.defaultConfig = {
  plugins: [
    DocumentList,
    Essentials,
    Bold,
    Italic,
    Paragraph,
    PasteFromOffice,
    Table,
    TableToolbar,
    TableProperties,
    TableCellProperties,
    Highlight,
    Alignment,
    TableColumnResize,
  ],
  table: {
    contentToolbar: [
      "tableColumn",
      "tableRow",
      "mergeTableCells",
      "tableProperties",
      "tableCellProperties",
    ],
    /*  list: {
      properties: {
        styles: true,
        startIndex: true,
        reversed: true,
      },
    }, */
    tableProperties: {
      // The default styles for tables in the editor.
      // They should be synchronized with the content styles.
      /*   defaultProperties: {
        borderStyle: "solid",
        borderColor: "hsl(0, 0%, 0%)",
        borderWidth: "1px",
        alignment: "left",
        width: "100%",
        //height: "450px",
      },
      // The default styles for table cells in the editor.
      // They should be synchronized with the content styles.
      tableCellProperties: {
        defaultProperties: {
          borderStyle: "solid",
          borderColor: "hsl(0, 0%, 0%)",
          borderWidth: "1px",
          //horizontalAlignment: "center",
          //verticalAlignment: "bottom",
          //padding: "10px",
        },
      }, */
    },
  },
  toolbar: {
    items: [
      "bold",
      "italic",
      "|",
      "insertTable",
      "bulletedList",
      "numberedList",
      "alignment",
      "highlight",
    ],
  },
};
