import axiosClient from '../api/axiosClient';

const vehicleService = {
    
    getVehicleTypes: async () => {
        return await axiosClient.get('/Vehicles/types');
    },

    
    createVehicle: async (vehicleData) => {
        
        return await axiosClient.post('/Vehicles', vehicleData);
    },

    
    createVehicleType: async (typeData) => {
        
        return await axiosClient.post('/Vehicles/types', typeData);
    },

    
    deleteVehicle: async (id) => {
        return await axiosClient.delete(`/Vehicles/${id}`);
    }
};

export default vehicleService;
