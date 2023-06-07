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

const ticketsByStatus = async (status) => {
  const response = await axios
    .get("/Ticket/GetAllTicketsByStatus", { status: status })
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      return err.response.data;
    });
  return response;
};

export { tickets, ticketsByStatus };
