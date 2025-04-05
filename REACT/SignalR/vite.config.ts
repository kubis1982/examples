import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      '/hub': {
        target: 'https://localhost:7246',
        secure: false, // akceptuj self-signed certyfikaty
        ws: true, // obsługa WebSockets
        changeOrigin: true
      }
    }
  },
})
