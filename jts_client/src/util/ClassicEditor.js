import { ClassicEditor as ClassicEditorBase } from "@ckeditor/ckeditor5-editor-classic";
import { Essentials } from "@ckeditor/ckeditor5-essentials";
import { Bold, Italic } from "@ckeditor/ckeditor5-basic-styles";
import { Paragraph } from "@ckeditor/ckeditor5-paragraph";

export default class ClassicEditor extends ClassicEditorBase {}

ClassicEditor.builtinPlugins = [Essentials, Bold, Italic, Paragraph];

ClassicEditor.defaultConfig = {
  toolbar: {
    items: ["bold", "italic", "undo", "redo"],
  },
  language: "en",
};
