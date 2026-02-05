import axiosClient from '../api/axiosClient';

const authService = {
    
    register: async (userData) => {
        
        return await axiosClient.post('/Auth/register', userData);
    },

    
    login: async (email, password) => {
        const response = await axiosClient.post('/Auth/login', { email, password });
        
        if (response.succeeded) {
            localStorage.setItem('token', response.data);
            
        }
        return response;
    },

    
    logout: () => {
        localStorage.removeItem('token');
    }
};

export default authService;
