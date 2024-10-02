import apiClient from "../components/AxiosClient/AxiosClient";

export const addOrder = async (orderData) => {
    try {
        const response = await apiClient.post('/order/AddOrder', orderData);
        return response.data
    } catch (error) {
        throw error;
    }
};

export const getUndelivered = async () => {
    try {
        const response = await apiClient.get('/order/Undelivered');
        return response.data
    } catch (error) {
        throw error;
    }
};

export const takeOrder = async (orderId) => {
    try {
        console.log(orderId)
        const response = await apiClient.post(`/order/TakeOrder?orderId=${orderId}`);
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const getCurrentOrder = async () => {
    try {
        const response = await apiClient.get(`/order/currentOrder`);
        console.log(response)
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const getOrderHistoryDeliverer = async () => {
    try {
        const response = await apiClient.get(`/order/historyDeliverer`);
        console.log(response)
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const getOrderHistoryUser = async () => {
    try {
        const response = await apiClient.get(`/order/history`);
        console.log(response)
        return response.data;
    } catch (error) {
        throw error;
    }
};