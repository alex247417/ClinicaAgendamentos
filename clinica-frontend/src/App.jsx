import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Login';
import CadastrarPaciente from './pages/CadastrarPaciente';
import Agendamentos from './pages/Agendamentos';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Navigate to="/login" />} />
                <Route path="/login" element={<Login />} />
                <Route path="/agendamentos" element={<Agendamentos />} />
                <Route path="/cadastrar-paciente" element={<CadastrarPaciente />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;