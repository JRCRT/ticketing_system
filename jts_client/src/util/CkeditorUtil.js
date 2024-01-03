import juice from "juice";

const CkeditorUtil = {
  getEditorStyles() {
    const cssTexts = [],
      rootCssTexts = [];

    for (const styleSheets of document.styleSheets) {
      if (styleSheets.ownerNode.hasAttribute("data-cke")) {
        for (const cssRule of styleSheets["cssRules"]) {
          if (cssRule.cssText.indexOf(".ck-content") !== -1) {
            cssTexts.push(cssRule.cssText);
          } else if (cssRule.cssText.indexOf(":root") !== -1) {
            rootCssTexts.push(cssRule.cssText);
          }
        }
      }
    }

    return cssTexts.length
      ? [...rootCssTexts, ...cssTexts].join(" ").trim()
      : "";
  },
  getContentWithLineStyles(editorContent) {
    return juice.inlineContent(
      `<div class="ck-content">${editorContent}<div>`,
      CkeditorUtil.getEditorStyles()
    );
  },
};

export default CkeditorUtil;
