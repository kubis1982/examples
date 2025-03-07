import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { useQuery , gql } from '@apollo/client';

// vi.mock('@apollo/client', () => ({
//   useQuery: vi.fn((query) => {
//     return { data: 'mocked data', loading: false, error: null };
//   }),
// }));

const GET_DOCUMENTS = gql`
  query GetDocuments {
    documents(
      order: [ {
         contractor:  {
            name: ASC
         }
      }]
    ) {
      nodes {
        id
        number
        executeDate
        contractor {
          id
          name
        }
        items {
          id
          quantity
          article {
            id
            name
            unit
          }
        }
      }
    }
  }
`;

function Documents() {
  const { loading, error, data } = useQuery(GET_DOCUMENTS);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return (
    <div style={{ textAlign: 'left' }}>
      <h2>Documenty</h2>
      {data.documents.map((document: any) => (
        <div key={document.id} >
          <p>Number: {document.number}</p>
          <p>Data realizacji: {document.executeDate}</p>
          <p>Nazwa kontrahenta: {document.contractor.name}</p>
          <ul>Pozycje: 
            {document.items.map((item: any) => (
              <li key={item.id} >
                <p>Ilość: {item.quantity}</p>
                <p>Kod: {item.article.code}</p>
                <p>Nazwa: {item.article.name}</p>
                <p>Jednostka: {item.article.unit}</p>
              </li>
            ))}
          </ul>
          </div>))}
          <h2>Artykuły</h2>
          <ul>
            {data.articles.map((article: any) => (
              <li key={article.code}>
                <p>Kod: {article.code}</p>
                <p>Nazwa: {article.name}</p>
              </li>
            ))}
            </ul>
    </div>
  );
}

function App() {
  const [count, setCount] = useState(0)
  
  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
      <Documents />
    </>
  )
}

export default App
