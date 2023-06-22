const FILE_STATUS = Object.freeze({
  INITIAL: "INITIAL",
  LOADING: "LOADING",
});

const EXTENSIONS = Object.freeze({
  DOCX: "docx",
  DOC: "doc",
  PDF: "pdf",
  XLS: "xls",
  XLSX: "xlsx",
});

const TICKET_STATUS = Object.freeze({
  PENDING: "Pending",
  APPROVED: "Approved",
  DECLINED: "Declined",
});

export { FILE_STATUS, EXTENSIONS, TICKET_STATUS };
