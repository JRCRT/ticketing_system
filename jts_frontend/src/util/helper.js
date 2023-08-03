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

const uploadFile = async (file, url) => {
  // set up the request data
  let formData = new FormData();
  formData.append("file", file.file);

  // track status and upload file
  // file.status = "loading";
  let response = await fetch(url, { method: "POST", body: formData });

  // change status to indicate the success of the upload request
  // file.status = response.ok;

  return response;
};

export { setIconUrl, uploadFile, formatDate };
