import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import HomePage from './pages/home.tsx'
import OrdersPage from './pages/orders.tsx'
import Dashboard from './layouts/dashboard.tsx'

const router = createBrowserRouter([
  {
    Component: App,
    children: [
      {
        path: '/',
        Component: Dashboard,
        children: [
          {
            path: '',
            Component: HomePage,
          },
          {
            path: 'orders',
            Component: OrdersPage,
          },
        ],
      }
    ],
  },
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)