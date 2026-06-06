import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Login';
import CadastrarPaciente from './pages/CadastrarPaciente';
import Agendamentos from './pages/Agendamentos';
import CadastrarProfissional from './pages/CadastrarProfissional';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Navigate to="/login" />} />
                <Route path="/login" element={<Login />} />
                <Route path="/agendamentos" element={<Agendamentos />} />
                <Route path="/cadastrar-paciente" element={<CadastrarPaciente />} />
                <Route path="/cadastrar-profissional" element={<CadastrarProfissional />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;