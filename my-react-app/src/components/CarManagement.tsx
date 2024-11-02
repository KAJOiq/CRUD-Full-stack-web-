import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

// تعريف نوع السيارة
interface Car {
    id?: number;
    ownerName: string;
    ownerSurename: string;
    carPlateNum: string;
    carBrandName: string;
    carModel: number;
    chassisNumber: string;
}

const CarManagement: React.FC = () => {
    const [form, setForm] = useState<Car>({
        ownerName: '',
        ownerSurename: '',
        carPlateNum: '',
        carBrandName: '',
        carModel: new Date().getFullYear(),
        chassisNumber: ''
    });
    
    const [cars, setCars] = useState<Car[]>([]);
    const [carById, setCarById] = useState<Car | null>(null);  // لحفظ بيانات السيارة التي يتم جلبها باستخدام id
    const [error, setError] = useState<string | null>(null);
    const [isEditing, setIsEditing] = useState<boolean>(false);
    const [currentCarId, setCurrentCarId] = useState<number | null>(null);
    const [fetchId, setFetchId] = useState<string>('');  // لتخزين id المدخل

    useEffect(() => {
        fetchCars();
    }, []);

    const fetchCars = async () => {
        try {
            const response = await axios.get('http://localhost:5000/api/Cars');
            setCars(response.data);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setError('Error fetching cars: ' + (error.response?.data.message || error.message));
            } else {
                setError('An unknown error occurred while fetching cars.');
            }
        }
    };

    // دالة لجلب السيارة باستخدام id
    const fetchCarById = async () => {
        if (!fetchId) return; // التأكد من إدخال id
        try {
            const response = await axios.get(`http://localhost:5000/api/Cars/${fetchId}`);
            setCarById(response.data);
            setError(null);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setError('Error fetching car by ID: ' + (error.response?.data.message || error.message));
            } else {
                setError('An unknown error occurred while fetching car by ID.');
            }
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setForm({
            ...form,
            [e.target.name]: e.target.value,
        });
    };

    const handleAddOrUpdateCar = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            if (isEditing && currentCarId) {
                await axios.put(`http://localhost:5000/api/Cars/${currentCarId}`, form);
            } else {
                await axios.post('http://localhost:5000/api/Cars/Register', form);
            }
            fetchCars();
            resetForm();
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setError('Failed to add/update car: ' + (error.response?.data.message || error.message));
            } else {
                setError('An unknown error occurred while adding/updating the car.');
            }
        }
    };

    const handleEditCar = (car: Car) => {
        setForm(car);
        setIsEditing(true);
        setCurrentCarId(car.id || null);
    };

    const handleDeleteCar = async (id: number) => {
        try {
            await axios.delete(`http://localhost:5000/api/Cars/${id}`);
            fetchCars();
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setError('Failed to delete car: ' + (error.response?.data.message || error.message));
            } else {
                setError('An unknown error occurred while deleting the car.');
            }
        }
    };

    const resetForm = () => {
        setForm({
            ownerName: '',
            ownerSurename: '',
            carPlateNum: '',
            carBrandName: '',
            carModel: new Date().getFullYear(),
            chassisNumber: ''
        });
        setError(null);
        setIsEditing(false);
        setCurrentCarId(null);
    };

    return (
        <div className="container mt-4">
            <h1 className="mb-4">Car Management</h1>
            
            <form onSubmit={handleAddOrUpdateCar} className="mb-4">
                <div className="row">
                    <div className="col-md-6 mb-3">
                        <input className="form-control" name="ownerName" value={form.ownerName} onChange={handleInputChange} placeholder="Owner Name" required />
                    </div>
                    <div className="col-md-6 mb-3">
                        <input className="form-control" name="ownerSurename" value={form.ownerSurename} onChange={handleInputChange} placeholder="Owner Surname" required />
                    </div>
                    <div className="col-md-6 mb-3">
                        <input className="form-control" name="carBrandName" value={form.carBrandName} onChange={handleInputChange} placeholder="Car Brand Name" required />
                    </div>
                    <div className="col-md-6 mb-3">
                        <input className="form-control" name="carPlateNum" value={form.carPlateNum} onChange={handleInputChange} placeholder="Car Plate Number" required />
                    </div>
                    <div className="col-md-6 mb-3">
                        <input className="form-control" name="chassisNumber" value={form.chassisNumber} onChange={handleInputChange} placeholder="Chassis Number" required />
                    </div>
                    <div className="col-md-6 mb-3">
                        <input className="form-control" type="number" name="carModel" value={form.carModel} onChange={handleInputChange} placeholder="Car Model (Year)" required />
                    </div>
                </div>
                <button className="btn btn-primary" type="submit">{isEditing ? 'Update Car' : 'Add Car'}</button>
                <button className="btn btn-secondary ms-2" type="button" onClick={resetForm}>Reset</button>
            </form>

            {error && <div style={{ color: 'red' }}>{error}</div>}
            
            <h2 className="mb-3">Fetch Car by ID</h2>
            <div className="input-group mb-3">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Enter Car ID"
                    value={fetchId}
                    onChange={(e) => setFetchId(e.target.value)}
                />
                <button className="btn btn-primary" onClick={fetchCarById}>Fetch Car</button>
            </div>
            
            {carById && (
                <div className="card mb-3">
                    <div className="card-body">
                        <h5 className="card-title">{carById.ownerName} {carById.ownerSurename}</h5>
                        <p className="card-text">Brand: {carById.carBrandName}</p>
                        <p className="card-text">Plate Number: {carById.carPlateNum}</p>
                        <p className="card-text">Model: {carById.carModel}</p>
                        <p className="card-text">Chassis Number: {carById.chassisNumber}</p>
                    </div>
                </div>
            )}

            <h2 className="mb-3">Car List</h2>
            <ul className="list-group">
                {cars.map((car) => (
                    <li key={car.id} className="list-group-item d-flex justify-content-between align-items-center">
                        {car.ownerName} {car.ownerSurename} - {car.carBrandName} ({car.carModel}) - {car.carPlateNum} - {car.chassisNumber}
                        <div>
                            <button className="btn btn-warning btn-sm me-2" onClick={() => handleEditCar(car)}>Edit</button>
                            <button className="btn btn-danger btn-sm" onClick={() => handleDeleteCar(car.id || 0)}>Delete</button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default CarManagement;
