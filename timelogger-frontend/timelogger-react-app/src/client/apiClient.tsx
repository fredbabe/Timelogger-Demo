import axios from "axios";
import { Client } from "./generated/api-client";

const ax = axios.create({
  withCredentials: false,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});

ax.interceptors.request.use(async (config) => {
  config.baseURL = "http://localhost:5222";
  ax.defaults.baseURL = config.baseURL;

  return Promise.resolve(config);
});

export const apiClient = new Client("", ax);
