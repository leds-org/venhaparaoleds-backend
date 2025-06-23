import express from 'express';
import candidateRoutes from "./modules/candidates/candidate.routes";
import contestRoutes from "./modules/contests/contest.routes";

const app = express();
app.use(express.json());

app.use("/api", candidateRoutes, contestRoutes);

export default app;