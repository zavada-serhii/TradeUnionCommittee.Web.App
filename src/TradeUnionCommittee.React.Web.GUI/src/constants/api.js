export const API = window._env_?.REACT_APP_MAIN_API_URL || "https://localhost:5002/api/";

export const TOKEN = `${API}v1/Account/Token`;
export const REFRESH_TOKEN = `${API}v1/Account/RefreshToken`;

export const GET_ALL_POSITIONS = `${API}v1/Position/GetAll`;
