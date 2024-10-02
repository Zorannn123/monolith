import React, { useState, useEffect } from "react";
import { useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import { getUndelivered, takeOrder } from "../../services/orderService";

export const UndeliveredOrders = () => {
    const [undeliveredOrders, setUndeliveredOrders] = useState([]);
    const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        const fetchAllUndelivered = async () => {
            try {
                const undelivered = await getUndelivered();
                const ordersWithTotalValue = undelivered.map(order => {
                    console.log(order)
                    let totalValue = order.orderParts.reduce((sum, part) => {
                        const price = parseFloat(part.product.price);
                        const quantity = part.quantity || 0;
                        if (!isNaN(price) && !isNaN(quantity)) {
                            return sum + (price * quantity);
                        }
                        return sum;
                    }, 0);
                    totalValue += 250;
                    console.log(totalValue)
                    return { ...order, value: totalValue };
                });
                console.log(ordersWithTotalValue)
                setUndeliveredOrders(ordersWithTotalValue);
            } catch (error) {
                setErrorMessage('Failed to fetch orders.');
                console.error('Error fetching orders: ', error);
            }
        };
        fetchAllUndelivered();
    }, []);

    const handlePickOrder = async (orderId) => {
        try {
            const response = await takeOrder(orderId);
            if (response) {
                setUndeliveredOrders((prevOrders) => prevOrders.filter(order => order.orderId !== orderId));
                window.alert('You took the order.');
                navigate('/');
            } else {
                alert("Cannot take multiple orders at the same time.");
            }
        } catch (error) {
            console.error(`Error taking order ${orderId}: `, error);
            setErrorMessage(`Failed to take order ${orderId}.`);
        }
    };


    return (
        <Box
            sx={{
                display: 'flex',
                minHeight: '100vh',
                flexDirection: 'column',
                alignItems: 'flex-start',
                padding: '20px',
                backgroundColor: "#f8f9fa"
            }}
        >
            <Typography variant="h4" gutterBottom
                sx={{ marginBottom: '20px', fontFamily: "Roboto", marginTop: '30px' }}>
                Undelivered Orders
            </Typography>

            {errorMessage && (
                <Typography variant="body1" color="error">
                    {errorMessage}
                </Typography>
            )}

            {undeliveredOrders.length > 0 ? (
                <div>
                    {undeliveredOrders.map((order) => (
                        <div key={order.orderId} style={{ border: 'double', margin: '10px', padding: '10px' }}>
                            <Typography variant="h6">Order ID: {order.orderId}</Typography>
                            <Typography>Address: {order.address}</Typography>
                            <Typography>Comment: {order.comment}</Typography>
                            <Typography>Total Value: {order.value}</Typography> {console.log(order)}
                            <Button
                                variant="contained"
                                color="primary"
                                onClick={() => handlePickOrder(order.orderId)}
                                sx={{ float: 'right', margin: '5px' }}
                            >
                                Take Order
                            </Button>
                        </div>
                    ))}
                </div>
            ) : (
                <Typography>No undelivered orders available.</Typography>
            )}

            <Button
                variant="outlined"
                sx={{ marginTop: '20px', backgroundColor: 'black', color: '#f7e32f' }}
                onClick={() => navigate("/")}
            >
                Back
            </Button>
        </Box>
    );
};