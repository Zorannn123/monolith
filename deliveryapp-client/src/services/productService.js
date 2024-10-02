import apiClient from "../components/AxiosClient/AxiosClient";

export const AddNewProduct = async (productData) => {
    try {
        const response = await apiClient.post('/products/addProduct', productData);
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const GetAllProducts = async () => {
    try {
        const response = await apiClient.get('/products/getAll');
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const DeleteProduct = async (id) => {
    try {
        const response = await apiClient.delete(`/products/deleteProduct/${id}`);
        console.log(response);
        return response.data;
    } catch (error) {
        console.error('Error deleting product:', error);
        throw error;
    }
};
