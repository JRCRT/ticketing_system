import { EXTENSIONS } from "./constant";
import DocsIcon from "@/assets/doc.png";
import ExcelIcon from "@/assets/xls.png";
import PdfIcon from "@/assets/pdf.png";

const setIconUrl = (file) => {
  switch (file.name.split(".").pop()) {
    case EXTENSIONS.DOC:
      return DocsIcon;
    case EXTENSIONS.DOCX:
      return DocsIcon;
    case EXTENSIONS.XLS:
      return ExcelIcon;
    case EXTENSIONS.XLSX:
      return ExcelIcon;
    case EXTENSIONS.PDF:
      return PdfIcon;
  }
};

const formatDate = (date) => {
  return Intl.DateTimeFormat("en-US").format(new Date(date));
};

export { setIconUrl, formatDate };
