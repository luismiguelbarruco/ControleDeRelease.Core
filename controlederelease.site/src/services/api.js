import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:64662/api'
});

export default api;