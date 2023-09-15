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

const formatDateTime = (date) =>
  new Intl.DateTimeFormat("en-US", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
    second: "2-digit",
  }).format(date);

const setPriorityColor = (priority) => {
  switch (priority) {
    case "High":
      return "#b22222";
    case "Medium":
      return "#eed202";
    case "Low":
      return "green";
  }
};

export { setIconUrl, formatDate, formatDateTime, setPriorityColor };
