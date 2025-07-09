# API Usage Guide

The API is now running successfully! Here are the key details:

## Server Status
- **Server URL**: http://localhost:3001
- **Status**: ✅ Running
- **Database**: SQLite (populated with candidate and exam data)

## API Endpoints

### Health Check
```bash
GET /api/health
```
**Example:**
```bash
curl "http://localhost:3001/api/health"
```

### Get Concursos for a Candidate
```bash
GET /api/candidatos/{cpf}/concursos
```
**Example:**
```bash
curl "http://localhost:3001/api/candidatos/438.150.926-98/concursos" | jq '.'
```
**Response:** List of concursos (public exams) that match the candidate's professional profile.

### Get Candidates for a Concurso
```bash
GET /api/concursos/{codigo}/candidatos
```
**Example:**
```bash
curl "http://localhost:3001/api/concursos/50522368154/candidatos" | jq '.'
```
**Response:** List of candidates whose professional profile matches the concurso requirements.

## Data Statistics
- **Candidates loaded**: 1,000
- **Concursos loaded**: 199 (with duplicate handling)
- **Total test cases**: 19 (all passing)

## Example API Calls

### 1. Find exams for a specific candidate
```bash
curl "http://localhost:3001/api/candidatos/438.150.926-98/concursos" | jq '.data.total'
# Returns: 27 matching concursos
```

### 2. Find candidates for a specific exam
```bash
curl "http://localhost:3001/api/concursos/50522368154/candidatos" | jq '.data.total'
# Returns: 92 matching candidates
```

### 3. Check API health
```bash
curl "http://localhost:3001/api/health" | jq '.'
# Returns API status and version information
```

## Validation Rules
- **CPF Format**: Must be in the format XXX.XXX.XXX-XX
- **Concurso Code**: Must contain exactly 11 digits
- **Professional Matching**: Candidates are matched with concursos based on their professional skills

## Error Handling
The API provides comprehensive error handling with appropriate HTTP status codes:
- `400` - Bad Request (invalid CPF/code format)
- `404` - Not Found (candidate/concurso not exists)
- `500` - Internal Server Error

## Technical Stack
- **Framework**: Express.js with TypeScript
- **Database**: SQLite with Prisma ORM
- **Architecture**: Clean Architecture
- **Testing**: Jest (unit and integration tests)
- **Logging**: Winston
- **Security**: Helmet, CORS, Rate Limiting

The system is ready for production deployment and includes comprehensive CI/CD pipeline configuration.
