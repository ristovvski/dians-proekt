import styled from "styled-components";

export const StyledButton = styled.button<{ color: string, background: string }>`
    color: ${props => props.color};
    background: ${props => props.background};

    padding: 10px 15px;
    border: none;
    cursor: pointer;
`;

export const Table = styled.table`
    border-collapse: collapse;
    border: 1px solid black;
    width: 90%;
    margin: auto;
`;

export const TableHead = styled.thead`
    border-collapse: collapse;
    border: 1px solid black;
`;

export const TableRow = styled.tr`
    border-collapse: collapse;
    border: 1px solid black;
`;

export const TableHeader = styled.th`
    border-collapse: collapse;
    border: 1px solid black;
`;

export const TableBody = styled.tbody`

`;

export const TableColumn = styled.td`
    text-align: center;
    border-collapse: collapse;
    border: 1px solid black;
`;