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

// App.tsx
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import CarManagement from './components/CarManagement';
import CarYousifManagement from './components/CarYousifManagement';
import 'bootstrap/dist/css/bootstrap.min.css';

const App: React.FC = () => {
    return (
        <Router>
            <div className="container">
                <nav>
                    <Link to="/" className="btn btn-primary me-2">My Cars</Link>
                    <Link to="/caryousif" className="btn btn-primary">Yousif Cars</Link>
                </nav>
                <Routes>
                    <Route path="/" element={<CarManagement />} />
                    <Route path="/caryousif" element={<CarYousifManagement />} />
                </Routes>
            </div>
        </Router>
    );
};

export default App;
