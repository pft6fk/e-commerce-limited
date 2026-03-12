import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { registerCustomer } from "../api/customers";

function RegisterCustomerPage() {
    const [form, setForm] = useState({
        firstName: "",
        lastName: "",
        email: ""
    });
    const navigate = useNavigate();

    function handleSubmit() {
        registerCustomer(form).then(() => navigate("/customers"));
    }

    return (
        <div>
            <h1>Register Customer</h1>
            <div>First Name</div>
            <input value={form.firstName} onChange={e => setForm({ ...form, firstName: e.target.value })} />
            <div>Last Name</div>
            <input value={form.lastName} onChange={e => setForm({ ...form, lastName: e.target.value })} />
            <div>Email</div>
            <input value={form.email} onChange={e => setForm({ ...form, email: e.target.value })} />
            <button onClick={handleSubmit}>Register</button>
        </div>
    );
}

export default RegisterCustomerPage;
