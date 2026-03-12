import { useState } from "react";
import { Customer } from "../types";
import { getCustomer } from "../api/customers";

function CustomersPage() {
    const [customerId, setCustomerId] = useState("");
    const [customer, setCustomer] = useState<Customer | null>(null);
    const [error, setError] = useState("");

    function handleSearch() {
        if (!customerId) return;
        setError("");
        setCustomer(null);
        getCustomer(customerId)
            .then(data => setCustomer(data))
            .catch(() => setError("Customer not found"));
    }

    return (
        <div>
            <h1>Customers</h1>
            <div>
                <input
                    type="text"
                    placeholder="Enter Customer ID"
                    value={customerId}
                    onChange={e => setCustomerId(e.target.value)}
                />
                <button onClick={handleSearch}>Search</button>
            </div>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {customer && (
                <div>
                    <h3>{customer.firstName} {customer.lastName}</h3>
                    <p>Email: {customer.email}</p>
                    <p>Registered: {customer.registeredAt}</p>
                </div>
            )}
        </div>
    );
}

export default CustomersPage;
