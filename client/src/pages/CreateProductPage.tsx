import { createProduct } from "../api/products";
import { useState } from "react";
import { useNavigate } from 'react-router-dom'

function CreateProductPage(){
    const [form, setForm] = useState({
        name: "",
        description: "",
        price: 0,
        currency: "USD"
    });
    const navigate = useNavigate();

    function createProductFunction(){
        createProduct(form).then(() => navigate("/products"))
    }
    return (
        <div>
            <div>Name</div>
            <input value={form.name} onChange={e => setForm({...form, name: e.target.value})} />
            <div>Description</div>
            <input value={form.description} onChange={e => setForm({...form, description: e.target.value})}/>
            <div>Price</div>
            <input value={form.price} type="number" onChange={e => setForm({...form, price: parseFloat(e.target.value)})}/>
            <div>Currency</div>
            <input value={form.currency} onChange={e => setForm({...form, currency: e.target.value})}/>
            <button onClick={createProductFunction}>Submit</button>
        </div>
    );
}


export default CreateProductPage