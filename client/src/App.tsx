import './App.css'
import CustomersPage from './pages/CustomersPage';
import OrdersPage from './pages/OrdersPage';
import ProductsPage from './pages/ProductsPage';
import CreateProductPage from './pages/CreateProductPage';
import RegisterCustomerPage from './pages/RegisterCustomerPage';
import { BrowserRouter, Route, Routes, Link, Navigate } from 'react-router-dom'


function App() {
    return (
      <BrowserRouter>
      <nav>
        <Link to="/products">Products</Link>
      </nav>
      <nav>
        <Link to="/customers">Customers</Link>
      </nav>
      <nav>
        <Link to="/orders">Orders</Link>
      </nav>
      <nav>
        <Link to="/create-product">Create Product</Link>
      </nav>
      <nav>
        <Link to="/register-customer">Register Customer</Link>
      </nav>
        <Routes>
          <Route path="/" element={<Navigate to="/products" />} />
          <Route path="/products" element={<ProductsPage/>}/>
          <Route path="/orders" element={<OrdersPage/>}/>
          <Route path="/customers" element={<CustomersPage/>}/>
          <Route path="/create-product" element={<CreateProductPage/>}/>
          <Route path="/register-customer" element={<RegisterCustomerPage/>}/>
        </Routes>

      </BrowserRouter>
    );
}

export default App;

