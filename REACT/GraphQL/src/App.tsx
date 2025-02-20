import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { gql, useQuery } from '@apollo/client';

const GET_DOCUMENTS = gql`
  query GetDocuments {
    documents(
      where: { 
        id: { eq: 1 },
      }
    ) {
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
          code
          name
        }
      }
    }
  }
`;

function Documents() {
  const { loading, error, data } = useQuery(GET_DOCUMENTS);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error : {error.message}</p>;

  return data.documents.map((document: any) => (
    <div key={document.id}>
      <h3>{document.number}</h3>
      <b>About this location:</b>
      <br />
    </div>
  ));
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
