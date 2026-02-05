import axios from 'axios';


const BASE_URL = 'http://localhost:5200/api';

const axiosClient = axios.create({
    baseURL: BASE_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});


axiosClient.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});


axiosClient.interceptors.response.use((response) => {
    
    if (response.data && response.data.succeeded === false) {
        return Promise.reject(response.data);
    }
    return response.data; 
}, (error) => {
    
    const customError = error.response ? error.response.data : { succeeded: false, message: 'Network Error' };
    return Promise.reject(customError);
});

export default axiosClient;
