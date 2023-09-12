import axios from "./api";

const tickets = async () => {
  const response = await axios
    .get("/Ticket/GetAllTickets")
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const myTickets = async (param) => {
  const response = await axios
    .post("/Ticket/GetTicketByUser", param)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const ticketsForApproval = async (param) => {
  const response = await axios
    .post("/Ticket/GetTicketsForApproval", param)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const uploadFile = async (file) => {
  const response = await axios
    .post("/File/UploadFiles", file)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const createTicket = async (ticket) => {
  const response = await axios
    .post("/Ticket/CreateTicket", ticket)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const ticketsByStatus = async (param) => {
  const response = await axios
    .post("/Ticket/GetAllTicketByStatus", param)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const ticketsToday = async (userId) => {
  const response = await axios
    .get(`/Ticket/GetAllTicketsToday?userId=${userId}`)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const ticketById = async (id) => {
  const response = await axios
    .post(`/Ticket/GetTicketById?id=${id}`)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const approveTicket = async (signatory) => {
  const response = await axios
    .post("/Ticket/ApproveTicket", signatory)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const rejectTicket = async (signatory) => {
  const response = await axios
    .post("/Ticket/RejectTicket", signatory)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const doneTicket = async (request) => {
  const response = await axios
    .post("/Ticket/DoneTicket", request)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export {
  tickets,
  ticketsByStatus,
  ticketsToday,
  ticketById,
  createTicket,
  uploadFile,
  myTickets,
  ticketsForApproval,
  approveTicket,
  rejectTicket,
  doneTicket,
};
