// Styles.js
import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  padding-left: 200px;
  ${"" /* min-height: 10vh; */}
  font-size: 20px;
`;
export const Radio = styled.input`
  display: none;
`;
export const Rating = styled.div`
  cursor: pointer;
`;
