import React, { useEffect, useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import UserService from '../../Services/user.service';

const UserRegistrationStats = () => {
    const [data, setData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await UserService.getUserRegistrationStats();
                setData(response);
            } catch (error) {
                console.error('Error fetching user stats', error);
            }
        };

        fetchData();
    }, []);

    return (
        <div className="container py-5 d-flex flex-column align-items-center" style={{ minHeight: "70vh" }} >
            <p className="display-6 text-center mb-4">Регистрации новых пользователей</p>
            <BarChart
                width={600}
                height={300}
                data={data}
                margin={{
                    top: 5, right: 30, left: 20, bottom: 5,
                }}
            >
                <XAxis
                    dataKey="date"
                    tick={{ fill: 'white' }}
                    label={{ value: 'Дата', angle: 0, position: 'insideMiddle', offset: 25, fill: 'white' }}
                    stroke="white"
                />
                <YAxis
                    tick={{ fill: 'white' }}
                    tickFormatter={(tick) => Math.round(tick)}
                    label={{ value: 'Количество', angle: -90, position: 'insideMiddle', offset: 25, fill: 'white' }}
                    allowDecimals={false}
                    stroke="white"
                />
                <Tooltip />
                <Bar dataKey="count" fill="#8884d8" />
            </BarChart>
        </div >
    );
};

export default UserRegistrationStats;
