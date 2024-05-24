# Random Hero Battle Simulator

This project is a web API that simulates battles between heroes in an arena. The heroes can be archers, horsemen, or swordsmen, each with unique attack and defense rules. The battle continues until at most one hero is left alive.

## Hero Attack Rules

### Archer Attacks:
- **Horseman**: 40% chance the horseman dies, 60% chance it's blocked.
- **Swordsman**: Swordsman dies.
- **Archer**: Defending archer dies.

### Swordsman Attacks:
- **Horseman**: No effect.
- **Swordsman**: Defending swordsman dies.
- **Archer**: Attacking archer dies.

### Horseman Attacks:
- **Horseman**: Defending horseman dies.
- **Swordsman**: Horseman dies.
- **Archer**: Attacking archer dies.

## Battle Rules

1. The battle is divided into rounds where a random attacker and defender are selected.
2. Heroes not involved in the round rest, increasing their health by 10 points (but not exceeding their initial maximum health).
3. The health of participating heroes is halved during the battle. If it becomes less than a quarter of the initial health, they die.
4. Initial health values:
    - Archer: 100
    - Horseman: 150
    - Swordsman: 120

## API Endpoints

### Random Hero Generator

- **Endpoint**: `/api/heroes/generate`
- **Method**: POST
- **Input**: `numberOfFighters` (query parameter)
- **Output**: Arena identifier

#### Example Request
```
- POST /api/heroes/generate?numberOfFighters=5
```

### Battle
- **Endpoint**: `/api/heroes/battle`
- **Method**: POST
- **Input**: `arenaId` (query parameter)
- **Output**: History describing the number of rounds, who attacked whom in each round, and how their health changed.

#### Example Request
```
- POST /api/heroes/battle?arenaId={Arena identifier}
```

### Focus on Requirements and Unit Testing
This project is laser-focused on meeting the specific requirements outlined for the battle simulation and ensuring robust unit testing. The core functionality and battle logic are designed to adhere strictly to these rules, providing a solid foundation for future enhancements.

### Temporary Data Storage
Currently, all data is stored in temporary in-memory structures to facilitate quick development and testing. This approach allows for immediate feedback and iteration on the core logic without the overhead of database management.

### Future Enhancements
While the current implementation focuses on the core battle logic and unit testing, future enhancements will include:
- Implementing interfaces to abstract data storage.
- Connecting to a database for persistent data storage.
- Enhancing performance and scalability.
- These improvements will ensure the simulator is robust, scalable, and ready for production use.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/random-hero-battle-simulator.git
   cd random-hero-battle-simulator
   ```
2. Build the project:
    ```
    dotnet build
    ````
3. Run the tests:
   ```
   dotnet test
   ```
4. Run the project:
   ```
   Run the project:
   ```
   
