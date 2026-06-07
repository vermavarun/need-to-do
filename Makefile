.PHONY: docker-build docker-run docker-stop clean help

DOCKER_IMAGE_NAME = need-to-do
DOCKER_CONTAINER_NAME = need-to-do-app
DOCKER_PORT = 8080

help:
	@echo "Available targets:"
	@echo "  docker-build   - Build Docker image"
	@echo "  docker-run     - Run Docker container"
	@echo "  docker-stop    - Stop and remove Docker container"
	@echo "  clean          - Remove Docker image and container"

docker-build:
	docker build -t $(DOCKER_IMAGE_NAME):latest .

docker-run: docker-build
	docker run -d -p $(DOCKER_PORT):8080 --name $(DOCKER_CONTAINER_NAME) $(DOCKER_IMAGE_NAME):latest

docker-stop:
	docker stop $(DOCKER_CONTAINER_NAME) || true
	docker rm $(DOCKER_CONTAINER_NAME) || true

clean: docker-stop
	docker rmi $(DOCKER_IMAGE_NAME):latest || true
