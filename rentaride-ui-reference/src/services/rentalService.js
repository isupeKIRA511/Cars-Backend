import axiosClient from '../api/axiosClient';

const rentalService = {
    
    createRental: async (rentalRequest) => {
        
        return await axiosClient.post('/Rentals', rentalRequest);
    },

    
    
};

export default rentalService;
