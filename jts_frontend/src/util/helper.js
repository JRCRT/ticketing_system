import { EXTENSIONS } from "./constant";
import DocsIcon from "@/assets/doc.png";
import ExcelIcon from "@/assets/xls.png";
import PdfIcon from "@/assets/pdf.png";

const setIcon = (file) => {
  switch (file.name.split(".").pop()) {
    case EXTENSIONS.DOC:
      return DocsIcon;
    case EXTENSIONS.EXCEL:
      return ExcelIcon;
    case EXTENSIONS.PDF:
      return PdfIcon;
  }
};

export { setIcon };
