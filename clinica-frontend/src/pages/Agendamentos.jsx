import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function Agendamentos() {
    const [profissionais, setProfissionais] = useState([]);
    const [pacientes, setPacientes] = useState([]);
    const [consultas, setConsultas] = useState([]);
    const [profissionalId, setProfissionalId] = useState('');
    const [pacienteId, setPacienteId] = useState('');
    const [dataHora, setDataHora] = useState('');
    const [dataBusca, setDataBusca] = useState(new Date().toISOString().split('T')[0]);

    
    const [foiBuscado, setFoiBuscado] = useState(false);

    const [erro, setErro] = useState('');
    const [sucesso, setSucesso] = useState('');
    const navigate = useNavigate();

    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };

    useEffect(() => {
        carregarProfissionais();
        carregarPacientes();
    }, []);

    const carregarProfissionais = async () => {
        try {
            const response = await api.get('/Profissionais', { headers });
            setProfissionais(response.data);
        } catch {
            setErro('Erro ao carregar profissionais.');
        }
    };
    const carregarPacientes = async () => {
        try {
            const response = await api.get('/Pacientes', { headers });
            setPacientes(response.data);
        } catch {
            setErro('Erro ao carregar pacientes.');
        }
    };
    
    const carregarAgenda = async () => {
        if (!profissionalId) {
            setErro('Selecione um profissional primeiro para ver a agenda.');
            return;
        }
        setErro(''); // Limpa erro anterior

        try {
            const response = await api.get(
                `/Consultas/agenda/${profissionalId}?data=${dataBusca}`,
                { headers }
            );
            setConsultas(response.data);
            setFoiBuscado(true); // Avisa que a busca foi concluída
        } catch {
            setErro('Erro ao carregar agenda.');
        }
    };

    const handleAgendar = async (e) => {
        e.preventDefault();
        setErro('');
        setSucesso('');
        
        const dataDigitada = new Date(dataHora);
        if (dataDigitada.getFullYear() > 2100) {
            setErro('Data inválida. Verifique o ano digitado.');
            return;
        }

        try {
            await api.post('/Consultas/agendar', {
                pacienteId: parseInt(pacienteId),
                profissionalId: parseInt(profissionalId),
                dataHoraInicio: dataHora
            }, { headers });

            setSucesso('Consulta agendada com sucesso!');
            carregarAgenda();
        } catch (error) {
            const dadosErro = error.response?.data;
            let mensagemErro = 'Erro ao agendar consulta.';

            if (typeof dadosErro === 'string') {
                mensagemErro = dadosErro;
            } else if (typeof dadosErro === 'object' && dadosErro !== null) {
                mensagemErro = dadosErro.title || dadosErro.detail || JSON.stringify(dadosErro);
            }

            setErro(mensagemErro);
        }
    };

    return (
        <div style={{ padding: '30px', maxWidth: '600px', margin: '0 auto' }}>
            <h2>Agendamentos</h2>

            <div style={{ display: 'flex', gap: '10px', marginBottom: '20px' }}>
                <button onClick={() => navigate('/cadastrar-paciente')}>
                    Cadastrar Paciente
                </button>
                <button onClick={() => navigate('/cadastrar-profissional')}>
                    Cadastrar Profissional
                </button>
                <button onClick={() => { localStorage.removeItem('token'); navigate('/login'); }}>
                    Sair
                </button>
            </div>

            <h3>Nova Consulta</h3>
            <form onSubmit={handleAgendar} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>

                
                <select
                    value={pacienteId}
                    onChange={(e) => setPacienteId(e.target.value)}
                    required
                >
                    <option value="">Selecione o paciente</option>
                    {pacientes.map(p => (
                        <option key={p.id} value={p.id}>{p.nome}</option>
                    ))}
                </select>

                
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
                <DatePicker
                    selected={dataHora ? new Date(dataHora) : null}
                    onChange={(date) => {
                        if (date) {
                            const ano = date.getFullYear();
                            if (ano > 9999) return;
                            const local = new Date(date.getTime() - date.getTimezoneOffset() * 60000)
                                .toISOString()
                                .slice(0, 16);
                            setDataHora(local);
                        }
                    }}
                    showTimeSelect
                    timeFormat="HH:mm"
                    timeIntervals={30}
                    dateFormat="dd/MM/yyyy HH:mm"
                    minDate={new Date()}
                    filterDate={(date) => date.getDay() !== 0 && date.getDay() !== 6}
                    minTime={new Date(new Date().setHours(8, 0, 0))}
                    maxTime={new Date(new Date().setHours(17, 30, 0))}
                    placeholderText="Selecione data e hora"
                    required
                />
                <button type="submit">Agendar</button>
            </form>

            {sucesso && <p style={{ color: 'green' }}>{sucesso}</p>}
            {erro && <p style={{ color: 'red' }}>{erro}</p>}

            <h3>Agenda do Profissional</h3>

            <div style={{ display: 'flex', gap: '10px', marginBottom: '20px' }}>
                <input
                    type="date"
                    value={dataBusca}
                    onChange={(e) => setDataBusca(e.target.value)}
                    style={{ padding: '8px', borderRadius: '4px', border: '1px solid #ccc' }}
                />
                <button onClick={carregarAgenda} style={{ padding: '8px 15px', cursor: 'pointer' }}>
                    Ver agenda do dia
                </button>
            </div>

            {/* Mensagem Bonita de Vazio */}
            {foiBuscado && consultas.length === 0 && (
                <div style={{ padding: '20px', backgroundColor: '#333', borderRadius: '8px', textAlign: 'center', color: '#aaa' }}>
                    Nenhum agendamento para este dia.
                </div>
            )}

            
            {consultas.length > 0 && (
                <ul style={{ listStyle: 'none', padding: 0, margin: 0 }}>
                    {consultas.map((c, i) => (
                        <li key={i} style={{
                            padding: '15px',
                            marginBottom: '10px',
                            backgroundColor: '#242424',
                            borderLeft: '5px solid #4CAF50',
                            borderRadius: '4px',
                            display: 'flex',
                            justifyContent: 'space-between',
                            alignItems: 'center'
                        }}>
                            <span style={{ fontWeight: 'bold', fontSize: '1.1em' }}>
                                {pacientes.find(p => p.id === c.pacienteId)?.nome || 'Paciente'} #{c.pacienteId}
                            </span>
                            <span style={{ backgroundColor: '#4CAF50', color: 'white', padding: '5px 10px', borderRadius: '15px', fontSize: '0.9em' }}>
                                {new Date(c.dataHoraInicio).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
                            </span>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}