<template>
  <Editor
    api-key="0y7yq1n1wb9a0dzwfvd0ii4znca2tn7uz6urusguy45j1di8"
    :init="{
      selector: 'textarea',
      plugins: 'lists table',
      menus: {
        edit: {
          title: 'Edit',
          items:
            'undo redo | cut copy paste pastetext | selectall | searchreplace',
        },
        format: {
          title: 'Format',
          items:
            'bold italic underline strikethrough superscript subscript codeformat | styles blocks fontfamily fontsize align lineheight | forecolor backcolor | language | removeformat',
        },
        table: {
          title: 'Table',
          items:
            'inserttable | cell row column | advtablesort | tableprops deletetable',
        },
      },
    }"
    :value="modelValue"
    @selection-change="emitContent"
  />
</template>
<script setup>
import Editor from "@tinymce/tinymce-vue";

defineProps(["modelValue"]);
const emit = defineEmits(["update:modelValue"]);

function emitContent(event, editor) {
  emit("update:modelValue", editor.getContent());
}
</script>

<style>
.tox .tox-statusbar__text-container {
  display: none !important;
}

/* .tox .tox-tinymce {
  height: 200px !important;
}

.tox .tox-editor-container {
  height: 100px !important;
} */
</style>
