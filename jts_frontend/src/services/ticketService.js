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

const myTickets = async (userId) => {
  const response = await axios
    .post(`/Ticket/GetTicketByUser?userId=${userId}`)
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

const ticketsByStatus = async (status) => {
  const response = await axios
    .post(`/Ticket/GetAllTicketByStatus?status=${status}`)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

const ticketsToday = async () => {
  const response = await axios
    .get("/Ticket/GetAllTicketsToday")
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

export {
  tickets,
  ticketsByStatus,
  ticketsToday,
  ticketById,
  createTicket,
  uploadFile,
  myTickets
};
