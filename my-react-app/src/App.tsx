/* import React from 'react';
import CarManagement from './components/CarManagement';
import 'bootstrap/dist/css/bootstrap.min.css'; // إضافة Bootstrap هنا

const App: React.FC = () => {
    return (
        <div>
            <CarManagement />
        </div>
    );
};

export default App;
 */

import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import CarManagement from './components/CarManagement';
import CarYousifManagement from './components/CarYousifManagement';
import Login from './components/Login';
import 'bootstrap/dist/css/bootstrap.min.css';

const App: React.FC = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);  // Track login status

    const handleLogin = (username: string, password: string) => {
        if ((username === 'Abdulrahman' && password === '12345') ||
            (username === 'Yousif' && password === '12345')) {
            setIsLoggedIn(true);
        }
    };

    const handleLogout = () => {
        setIsLoggedIn(false);
    };

    return (
        <Router>
            <div className="container">
                <nav className="my-3">
                    {!isLoggedIn ? (
                        <Link to="/login" className="btn btn-primary">Login</Link>
                    ) : (
                        <>
                            <Link to="/" className="btn btn-primary me-2">My Cars</Link>
                            <Link to="/caryousif" className="btn btn-primary me-2">Yousif Cars</Link>
                            <button onClick={handleLogout} className="btn btn-danger">Logout</button>
                        </>
                    )}
                </nav>
                
                <Routes>
                    <Route path="/login" element={<Login onLogin={handleLogin} />} />
                    <Route path="/" element={isLoggedIn ? <CarManagement /> : <Navigate to="/login" />} />
                    <Route path="/caryousif" element={isLoggedIn ? <CarYousifManagement /> : <Navigate to="/login" />} />
                </Routes>
            </div>
        </Router>
    );
};

export default App;
