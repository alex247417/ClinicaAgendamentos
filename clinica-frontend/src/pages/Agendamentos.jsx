import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function Agendamentos() {
    const [profissionais, setProfissionais] = useState([]);
    const [consultas, setConsultas] = useState([]);
    const [profissionalId, setProfissionalId] = useState('');
    const [pacienteId, setPacienteId] = useState('');
    const [dataHora, setDataHora] = useState('');
    const [erro, setErro] = useState('');
    const [sucesso, setSucesso] = useState('');
    const navigate = useNavigate();

    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };

    useEffect(() => {
        carregarProfissionais();
    }, []);

    const carregarProfissionais = async () => {
        try {
            const response = await api.get('/Profissionais', { headers });
            setProfissionais(response.data);
        } catch {
            setErro('Erro ao carregar profissionais.');
        }
    };

    const carregarAgenda = async () => {
        if (!profissionalId) return;
        try {
            const data = new Date().toISOString().split('T')[0];
            const response = await api.get(
                `/Consultas/agenda/${profissionalId}?data=${data}`,
                { headers }
            );
            setConsultas(response.data);
        } catch {
            setErro('Erro ao carregar agenda.');
        }
    };

    const handleAgendar = async (e) => {
        e.preventDefault();
        setErro('');
        setSucesso('');
        try {
            await api.post('/Consultas/agendar', {
                pacienteId: parseInt(pacienteId),
                profissionalId: parseInt(profissionalId),
                dataHoraInicio: dataHora
            }, { headers });
            setSucesso('Consulta agendada com sucesso!');
            carregarAgenda();
        } catch (error) {
            setErro(error.response?.data || 'Erro ao agendar consulta.');
        }
    };

    return (
        <div style={{ padding: '30px', maxWidth: '600px', margin: '0 auto' }}>
            <h2>Agendamentos</h2>
            <button onClick={() => navigate('/cadastrar-paciente')}>
                Cadastrar Paciente
            </button>
            <button onClick={() => { localStorage.removeItem('token'); navigate('/login'); }}
                    style={{ marginLeft: '10px' }}>
                Sair
            </button>

            <h3>Nova Consulta</h3>
            <form onSubmit={handleAgendar} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                <input
                    type="number"
                    placeholder="ID do Paciente"
                    value={pacienteId}
                    onChange={(e) => setPacienteId(e.target.value)}
                    required
                />
                <select
                    value={profissionalId}
                    onChange={(e) => setProfissionalId(e.target.value)}
                    required
                >
                    <option value="">Selecione o profissional</option>
                    {profissionais.map(p => (
                        <option key={p.id} value={p.id}>{p.nome} - {p.especialidade}</option>
                    ))}
                </select>
                <input
                    type="datetime-local"
                    value={dataHora}
                    onChange={(e) => setDataHora(e.target.value)}
                    required
                />
                <button type="submit">Agendar</button>
            </form>

            {sucesso && <p style={{ color: 'green' }}>{sucesso}</p>}
            {erro && <p style={{ color: 'red' }}>{erro}</p>}

            <h3>Agenda do Profissional</h3>
            <button onClick={carregarAgenda}>Ver agenda de hoje</button>
            <ul>
                {consultas.map((c, i) => (
                    <li key={i}>
                        Paciente {c.pacienteId} — {new Date(c.dataHoraInicio).toLocaleTimeString()}
                    </li>
                ))}
            </ul>
        </div>
    );
}