import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Typography from '@mui/material/Typography';
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import { getUserProfile } from "../../services/userService";

export const UserProfile = () => {
    const [username, setUsername] = useState('');
    const [firstName, setFirstName] = useState('');
    const [email, setEmail] = useState('');
    const [lastName, setLastName] = useState('');
    const [address, setAddress] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    //const [image, setImage] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();

    const fetchUserProfile = async () => {
        try {
            const response = await getUserProfile();
            const userProfile = response;
            console.log(response);
            if (userProfile) {
                setUsername(userProfile.username);
                setFirstName(userProfile.firstname);
                setEmail(userProfile.email);
                setLastName(userProfile.lastname);
                setAddress(userProfile.address);
                //setImage(userProfile.photoUrl);
                setDateOfBirth(userProfile.birthDate);
                localStorage.setItem('userImg', userProfile.photoUrl);
            }
            else {
                setErrorMessage('Error fetching user profile:');
            }
        } catch (error) {
            console.error('Error fetching user profile:', error);
        }

        //console.log("image", image)
    };

    useEffect(() => {
        fetchUserProfile();
    }, []);

    const handleEditClick = () => {
        navigate("/editProfile");
    }
    const HandleBack = () => {
        navigate("/");
    }

    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                height: '100vh',
                textAlign: 'center',
                marginTop: '-40px',
                padding: 2,
                backgroundColor: "#f8f9fa"
            }}
        >
            <Typography variant="h4" component="h1" sx={{ marginBottom: '50px', fontSize: '30px', fontFamily: "Roboto" }}>
                YOUR PROFILE
            </Typography>
            {/* <Avatar
                sx={{ width: 200, height: 200, marginBottom: '16px' }}
                alt="Profile Image"
                src={image}
            /> */}
            <Typography variant="body1" sx={{ marginBottom: '8px', fontSize: '20px', fontFamily: "Roboto" }}>
                Username: {username}
            </Typography>
            <Typography variant="body1" sx={{ marginBottom: '8px', fontSize: '20px', fontFamily: "Roboto" }}>
                First name: {firstName}
            </Typography>
            <Typography variant="body1" sx={{ marginBottom: '8px', fontSize: '20px', fontFamily: "Roboto" }}>
                Email: {email}
            </Typography>
            <Typography variant="body1" sx={{ marginBottom: '8px', fontSize: '20px', fontFamily: "Roboto" }}>
                Last name: {lastName}
            </Typography>
            <Typography variant="body1" sx={{ marginBottom: '8px', fontSize: '20px', fontFamily: "Roboto" }}>
                Address: {address}
            </Typography>
            <Typography variant="body1" sx={{ marginBottom: '30px', fontSize: '20px', fontFamily: "Roboto" }}>
                Date of Birth: {dateOfBirth}
            </Typography>

            {
                errorMessage && (
                    <Typography variant="body2" color="error" sx={{ marginBottom: '16px' }}>
                        {errorMessage}
                    </Typography>
                )
            }
            <Button
                variant="outlined"
                sx={{ marginBottom: '8px', backgroundColor: '#f7e32f', color: 'black' }}
                onClick={(e) => {
                    e.preventDefault();
                    handleEditClick();
                }}
            >
                Edit Profile
            </Button>
            <Button
                variant="outlined"
                sx={{ marginBottom: '8px', backgroundColor: 'black', color: '#f7e32f' }}
                onClick={HandleBack}
            >
                Back
            </Button>
        </Box >
    );
};