<template>
  <ckeditor
    :editor="editor"
    :config="editorConfig"
    @input="$emit('inputChange')"
  ></ckeditor>
</template>

<script setup>
//import Editor from "@tinymce/tinymce-vue";
import ClassicEditor from "@ckeditor/ckeditor5-editor-classic/src/classiceditor";

import EssentialsPlugin from "@ckeditor/ckeditor5-essentials/src/essentials";
import BoldPlugin from "@ckeditor/ckeditor5-basic-styles/src/bold";
import ItalicPlugin from "@ckeditor/ckeditor5-basic-styles/src/italic";
import ParagraphPlugin from "@ckeditor/ckeditor5-paragraph/src/paragraph";
import PasteFromOffice from "@ckeditor/ckeditor5-paste-from-office/src/pastefromoffice";
import Table from "@ckeditor/ckeditor5-table/src/table";
import TableToolbar from "@ckeditor/ckeditor5-table/src/tabletoolbar";
import TableProperties from "@ckeditor/ckeditor5-table/src/tableproperties";
import TableCellProperties from "@ckeditor/ckeditor5-table/src/tablecellproperties";
import Highlight from "@ckeditor/ckeditor5-highlight/src/highlight";
import Alignment from "@ckeditor/ckeditor5-alignment/src/alignment";
import TableColumnResize from "@ckeditor/ckeditor5-table/src/tablecolumnresize";

defineEmits(["inputChange"]);
const editor = ClassicEditor;
const editorConfig = {
  plugins: [
    EssentialsPlugin,
    BoldPlugin,
    ItalicPlugin,
    ParagraphPlugin,
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
  },
  toolbar: {
    items: ["bold", "italic", "|", "insertTable", "alignment", "highlight"],
  },
};
</script>
